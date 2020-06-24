using System;
using System.IO;
using System.Collections.Generic;

public class MiniCompiler
{

    public static int errors = 0;

    public static List<SyntaxTree> code = new List<SyntaxTree>();

    public static List<string> source;

    // arg[0] określa plik źródłowy
    // pozostałe argumenty są ignorowane
    public static int Main(string[] args)
    {
        string file;
        FileStream source;
        Console.WriteLine("\nLLVM Code Generator for Multiline Calculator - Recursive Descent Method");
        if (args.Length >= 1)
            file = args[0];
        else
        {
            Console.Write("\nsource file:  ");
            file = Console.ReadLine();
        }
        try
        {
            var sr = new StreamReader(file);
            string str = sr.ReadToEnd();
            sr.Close();
            Compiler.source = new System.Collections.Generic.List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
            source = new FileStream(file, FileMode.Open);
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" + e.Message);
            return 1;
        }
        Scanner scanner = new Scanner(source);
        Parser parser = new Parser(scanner);
        Console.WriteLine();
        parser.Parse();
        source.Close();
        if (errors == 0)
        {
            sw = new StreamWriter(file + ".ll");
            GenCode();
            sw.Close();
            Console.WriteLine("  compilation successful\n");
        }
        else
            Console.WriteLine($"\n  {errors} errors detected\n");
        return errors == 0 ? 0 : 2;
    }

    public static void EmitCode(string instr = null)
    {
        sw.WriteLine(instr);
    }

    public static void EmitCode(string instr, params object[] args)
    {
        sw.WriteLine(instr, args);
    }

    public static string NewTemp()
    {
        return string.Format($"%t{++nr}");
    }

    private static StreamWriter sw;
    private static int nr;

    private static void GenCode()
    {
        EmitCode("; prolog");
        EmitCode("@int_res = constant [15 x i8] c\"  Result:  %d\\0A\\00\"");
        EmitCode("@double_res = constant [16 x i8] c\"  Result:  %lf\\0A\\00\"");
        EmitCode("@end = constant [20 x i8] c\"\\0AEnd of execution\\0A\\0A\\00\"");
        EmitCode("declare i32 @printf(i8*, ...)");
        EmitCode("define void @main()");
        EmitCode("{");
        for (char c = 'a'; c <= 'z'; ++c)
        {
            EmitCode($"%i{c} = alloca i32");
            EmitCode($"store i32 0, i32* %i{c}");
            EmitCode($"%r{c} = alloca double");
            EmitCode($"store double 0.0, double* %r{c}");
        }
        EmitCode();

        for (int i = 0; i < code.Count; ++i)
        {
            EmitCode($"; linia {i + 1,3} :  " + source[i]);
            code[i].GenCode();
            EmitCode();
        }
        EmitCode("}");
    }

}

public abstract class SyntaxTree
{
    public char type;
    public int line = -1;
    public abstract char CheckType();
    public abstract string GenCode();
}

class Print : SyntaxTree
{

    private SyntaxTree exp;

    public Print(SyntaxTree e) { exp = e; }

    public override char CheckType() { exp.CheckType(); return ' '; }

    public override string GenCode()
    {
        string t;
        t = exp.GenCode();
        if (exp.type == 'i')
            Compiler.EmitCode($"call i32 (i8*, ...) @printf(i8* bitcast ([15 x i8]* @int_res to i8*), i32 {t})");
        else
            Compiler.EmitCode($"call i32 (i8*, ...) @printf(i8* bitcast ([16 x i8]* @double_res to i8*), double {t})");
        return null;
    }

}

class Assign : SyntaxTree
{

    private string id;
    private SyntaxTree exp;

    public Assign(string i, SyntaxTree e, int l) { id = i; exp = e; line = l; }

    public override char CheckType()
    {
        exp.CheckType();
        if (id[0] == '@' && exp.type != 'i')
            throw new ErrorException($"  line {line,3}:  semantic error - cannot convert real to int", false);
        return ' ';
    }

    public override string GenCode()
    {
        string t1, t2;
        t1 = exp.GenCode();
        if (id[0] == '$' && exp.type != 'r')
        {
            t2 = Compiler.NewTemp();
            Compiler.EmitCode($"{t2} = sitofp i32 {t1} to double");
        }
        else
            t2 = t1;
        Compiler.EmitCode("store {0} {1}, {0}* %{2}{3}", id[0] == '@' ? "i32" : "double", t2, id[0] == '@' ? 'i' : 'r', id[1]);
        return null;
    }

}

class Exit : SyntaxTree
{

    public override char CheckType() { return ' '; }  // operacja pusta - typy są sprawdzane tylko dla wyrażeń

    public override string GenCode()
    {
        Compiler.EmitCode("call i32 (i8*, ...) @printf(i8* bitcast ([20 x i8]* @end to i8*))");
        Compiler.EmitCode("ret void");
        return null;
    }

}

class BinaryOp : SyntaxTree
{

    private SyntaxTree left;
    private SyntaxTree right;
    private Tokens kind;

    public BinaryOp(SyntaxTree l, SyntaxTree r, Tokens k, int ln) { left = l; right = r; kind = k; line = ln; }

    public override char CheckType()
    {
        left.CheckType();
        right.CheckType();
        type = left.type == 'i' && right.type == 'i' ? 'i' : 'r';
        return type;
    }

    public override string GenCode()
    {
        string tw, t1, t2, t3, t4, tt;

        t1 = left.GenCode();
        if (left.type != type)
        {
            t2 = Compiler.NewTemp();
            Compiler.EmitCode($"{t2} = sitofp i32 {t1} to double");
        }
        else
            t2 = t1;
        t3 = right.GenCode();
        if (right.type != type)
        {
            t4 = Compiler.NewTemp();
            Compiler.EmitCode($"{t4} = sitofp i32 {t3} to double");
        }
        else
            t4 = t3;

        tw = Compiler.NewTemp();
        tt = type == 'i' ? "i32" : "double";
        switch (kind)
        {
            case Tokens.Plus:
                Compiler.EmitCode("{0} = {1} {2}, {3}", tw, type == 'i' ? "add i32" : "fadd double", t2, t4);
                break;
            case Tokens.Minus:
                Compiler.EmitCode("{0} = {1} {2}, {3}", tw, type == 'i' ? "sub i32" : "fsub double", t2, t4);
                break;
            case Tokens.Multiplies:
                Compiler.EmitCode("{0} = {1} {2}, {3}", tw, type == 'i' ? "mul i32" : "fmul double", t2, t4);
                break;
            case Tokens.Divides:
                Compiler.EmitCode("{0} = {1} {2}, {3}", tw, type == 'i' ? "sdiv i32" : "fdiv double", t2, t4);
                break;
            default:
                throw new ErrorException($"  line {line,3}:  internal gencode error", false);
        }
        return tw;
    }

}

class IntNumber : SyntaxTree
{

    private int ival;

    public IntNumber(int v) { ival = v; }

    public override char CheckType()
    {
        type = 'i';
        return 'i';
    }

    public override string GenCode()
    {
        return ival.ToString();
    }

}

class RealNumber : SyntaxTree
{

    private double rval;

    public RealNumber(double v) { rval = v; }

    public override char CheckType()
    {
        type = 'r';
        return 'r';
    }

    public override string GenCode()
    {
        return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.0###############}", rval);
    }

}

class Ident : SyntaxTree
{

    private string id;

    public Ident(string s, int l) { id = s; line = l; }

    public override char CheckType()
    {
        type = id[0] == '@' ? 'i' : 'r';
        return type;
    }

    public override string GenCode()
    {
        string w = Compiler.NewTemp();
        Compiler.EmitCode("{0} = load {1}, {1}* %{2}{3}", w, type == 'i' ? "i32" : "double", id[0] == '@' ? 'i' : 'r', id[1]);
        return w;
    }

}

class ErrorException : ApplicationException
{
    public readonly bool Recovery;
    public ErrorException(bool rec = true) { ++Compiler.errors; Recovery = rec; }
    public ErrorException(string msg, bool rec = true) : base(msg) { ++Compiler.errors; Recovery = rec; }
    public ErrorException(string msg, Exception ex, bool rec = true) : base(msg, ex) { ++Compiler.errors; Recovery = rec; }
}