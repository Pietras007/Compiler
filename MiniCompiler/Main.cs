using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCompiler
{
    public enum StatementType
    {
        EmptyStatement,
        IfStatement,
        IfElseStatement,
        WhileStatement,
        ReturnStatement,
        WriteStatement,
        ReadStatement,
        Declaration,
        Expression
    }

    public enum OperationType
    {
        Assign,
        Equal,
        Inequal,
        GreaterThan,
        GreaterOrEqual,
        LessThan,
        LessOrEqual,
        Plus,
        Minus,
        Multiplication,
        Divide,
        BooleanLogicalOr,
        BooleanLogicalAnd,
        ConditionalOr,
        ConditionalAnd,
        BitwiseComplement,
        LogicalNegation,
        IntConversion,
        DoubleConversion,
        UnaryMinus
    }

    public enum TypeOfValue
    {
        bool_val,
        int_val,
        double_val,
        string_val,
        identificator,
        wrong_val,
        assignment
    }

    public abstract class Statement
    {
        public StatementType Type;
        public abstract bool Check();
    }

    public class Empty_Statement : Statement
    {
        public Empty_Statement()
        {
            Type = StatementType.EmptyStatement;
        }

        public override bool Check()
        {
            return true;
        }
    }

    public class If_Statement : Statement
    {
        public Expression _ex;
        public Statement _st;
        public Statement _else_st;

        public If_Statement(Expression ex, Statement st)
        {
            _ex = ex;
            _st = st;
            Type = StatementType.IfStatement;
        }

        public If_Statement(Expression ex, Statement st, Statement else_st)
        {
            _ex = ex;
            _st = st;
            _else_st = else_st;
            Type = StatementType.IfElseStatement;
        }

        public override bool Check()
        {
            if (_ex.GetValueType() != TypeOfValue.bool_val)
            {
                Console.WriteLine("Error in expression (If_Statement) in line: " + _ex.Lineno);
                Compiler.typeErrors++;
                _st.Check();
                if (_else_st != null) _else_st.Check();
                return false;
            }

            if (_ex.GetValueType() == TypeOfValue.bool_val && _st.Check() && (_else_st == null || _else_st.Check()))
            {
                return true;
            }

            return false;
        }
    }

    public class While_Statement : Statement
    {
        public Expression _ex;
        public Statement _st;

        public While_Statement(Expression ex, Statement st)
        {
            _ex = ex;
            _st = st;
            Type = StatementType.WhileStatement;
        }

        public override bool Check()
        {
            if (_ex.GetValueType() != TypeOfValue.bool_val)
            {
                Console.WriteLine("Error in expression (While_Statement) in line: " + _ex.Lineno);
                Compiler.typeErrors++;
                _st.Check();
                return false;
            }

            if (_ex.GetValueType() == TypeOfValue.bool_val && _st.Check())
            {
                return true;
            }

            return false;
        }
    }

    public class Declaration_Statement : Statement
    {
        public Statement _st;
        public TypeOfValue _type;
        public Value _val_name;
        public Declaration_Statement(TypeOfValue type, Value val_name)
        {
            _type = type;
            _val_name = val_name;
            _st = null;
            Type = StatementType.Declaration;
            Compiler.identificators.Add(_val_name._identificator);
            Compiler.identificatorLines.Add((_val_name._identificator, _val_name.Lineno));
            if (!Compiler.identificatorValueType.ContainsKey(_val_name._identificator))
            {
                Compiler.identificatorValueType.Add(_val_name._identificator, type);
            }
        }

        public Declaration_Statement(TypeOfValue type, Value val_name, Statement st)
        {
            _type = type;
            _val_name = val_name;
            _st = st;
            Type = StatementType.Declaration;
            Compiler.identificators.Add(_val_name._identificator);
            Compiler.identificatorLines.Add((_val_name._identificator, _val_name.Lineno));
            if (!Compiler.identificatorValueType.ContainsKey(_val_name._identificator))
            {
                Compiler.identificatorValueType.Add(_val_name._identificator, type);
            }
        }

        public override bool Check()
        {
            if (Compiler.languageKeyWords.Contains(_val_name._identificator))
            {
                Console.WriteLine("Using 'language key word' in line: " + _val_name.Lineno);
                Compiler.typeErrors++;
                if (_st != null) _st.Check();
                return false;
            }

            int amount = 0;
            for (int i = 0; i < Compiler.identificators.Count; i++)
            {
                if (Compiler.identificators[i] == _val_name._identificator)
                {
                    amount++;
                }
            }

            if (amount > 1)
            {
                var _listelements = Compiler.identificatorLines.Where(x => x.Item1 == _val_name._identificator).ToList();
                var sortedlist = _listelements.OrderByDescending(x => x.Item2).ToArray();
                if (_val_name.Lineno == sortedlist[0].Item2)
                {
                    for(int i= sortedlist.Length - 2; i>=0;i--)
                    {
                        Console.WriteLine("Such declaration already exists. Error in line: " + sortedlist[i].Item2);
                    }
                }
                
                Compiler.typeErrors++;
                if (_st != null)
                {
                    _st.Check();
                }
                return false;
            }

            if (_st == null || _st.Check())
            {
                return true;
            }

            return false;
        }
    }

    public class Return_Statement : Statement
    {
        public Return_Statement()
        {
            Type = StatementType.ReturnStatement;
        }

        public override bool Check()
        {
            return true;
        }
    }

    public class Write_Statement : Statement
    {
        public Expression _ex;
        public Statement _st;
        public string _string;
        public Write_Statement(Expression ex)
        {
            _ex = ex;
            _st = null;
            _string = null;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(Expression ex, Statement st)
        {
            _ex = ex;
            _st = st;
            _string = null;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(string _str)
        {
            _string = _str;
            _st = null;
            _ex = null;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(string _str, Statement st)
        {
            _string = _str;
            _st = st;
            _ex = null;
            Type = StatementType.WriteStatement;
        }

        public override bool Check()
        {
            if (_string == null)
            {
                var valType = _ex.GetValueType();
                if (valType == TypeOfValue.wrong_val || valType == TypeOfValue.assignment)
                { 
                    Console.WriteLine("Wrong type in write expression in line: " + _ex.Lineno);
                    Compiler.typeErrors++;
                    if (_st != null) _st.Check();
                    return false;
                }
            }

            if (_string != null && (_st == null || _st.Check()))
            {
                return true;
            }

            if (_string == null && (_st == null || _st.Check()))
            {
                return true;
            }

            return false;
        }
    }

    public class Read_Statement : Statement
    {
        public Value _ident;
        public Statement _st;
        public Read_Statement(Value ident)
        {
            _ident = ident;
            Type = StatementType.ReadStatement;
        }

        public Read_Statement(Value ident, Statement st)
        {
            _ident = ident;
            _st = st;
            Type = StatementType.ReadStatement;
        }

        public override bool Check()
        {
            if (!Compiler.identificatorValueType.ContainsKey(_ident._identificator))
            {
                Console.WriteLine("Trying to read into uninitialized variable in line: " + _ident.Lineno);
                Compiler.typeErrors++;
                if (_st != null) _st.Check();
                return false;
            }
            else if (_st == null || _st.Check())
            {
                return true;
            }

            return false;
        }
    }

    public class Expression_Statement : Statement
    {
        Expression _ex;
        Statement _st;

        public Expression_Statement(Expression ex)
        {
            _ex = ex;
            Type = StatementType.Expression;
        }

        public Expression_Statement(Expression ex, Statement st)
        {
            _ex = ex;
            _st = st;
            Type = StatementType.Expression;
        }

        public override bool Check()
        {
            var valType = _ex.GetValueType();
            if (valType != TypeOfValue.wrong_val)
            {
                if (_st != null) _st.Check();
                return true;
            }

            if (_st != null) _st.Check();
            return false;
        }
    }

    public abstract class Expression
    {
        public TypeOfValue Type;
        public abstract TypeOfValue GetValueType();
        public int Lineno;
    }


    public class Operand : Expression
    {
        public OperationType _type;
        public Expression _exL;
        public Expression _exR;

        public Operand(Expression exL, OperationType type, Expression exR, int lineno)
        {
            _exL = exL;
            _exR = exR;
            _type = type;
            Lineno = lineno;
        }

        public override TypeOfValue GetValueType()
        {
            if (_type == OperationType.Assign)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (_exL.Type == TypeOfValue.identificator)
                {
                    if (valL == valR || (valL == TypeOfValue.double_val && valR == TypeOfValue.double_val))
                    {
                        return TypeOfValue.assignment;
                    }
                }
            }

            if (_type == OperationType.BooleanLogicalOr || _type == OperationType.BooleanLogicalAnd)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (valL == TypeOfValue.int_val && valR == TypeOfValue.int_val)
                {
                    return TypeOfValue.int_val;
                }
            }

            if (_type == OperationType.Plus || _type == OperationType.Minus || _type == OperationType.Multiplication || _type == OperationType.Divide)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (valL == TypeOfValue.int_val && valR == TypeOfValue.int_val)
                {
                    return TypeOfValue.int_val;
                }
                else if ((valL == TypeOfValue.int_val && valR == TypeOfValue.double_val) || (valL == TypeOfValue.double_val && valR == TypeOfValue.int_val) || (valL == TypeOfValue.double_val && valR == TypeOfValue.double_val))
                {
                    return TypeOfValue.double_val;
                }
            }

            if (_type == OperationType.Equal || _type == OperationType.Inequal || _type == OperationType.GreaterThan || _type == OperationType.GreaterOrEqual || _type == OperationType.LessThan || _type == OperationType.LessOrEqual)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if ((valL == TypeOfValue.int_val || valL == TypeOfValue.double_val) && (valR == TypeOfValue.int_val || valR == TypeOfValue.double_val))
                {
                    return TypeOfValue.bool_val;
                }
            }

            if (_type == OperationType.Equal || _type == OperationType.Inequal)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (valL == TypeOfValue.bool_val && valR == TypeOfValue.bool_val)
                {
                    return TypeOfValue.bool_val;
                }
            }

            if (_type == OperationType.ConditionalOr || _type == OperationType.ConditionalAnd)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (valL == TypeOfValue.bool_val && valR == TypeOfValue.bool_val)
                {
                    return TypeOfValue.bool_val;
                }
            }

            Console.WriteLine("Wrong type in operation expression " + _type.ToString() + " in line: " + Lineno);
            Compiler.typeErrors++;
            return TypeOfValue.wrong_val;
        }
    }

    public class UnaryOperand : Expression
    {
        public OperationType _type;
        public Expression _exR;

        public UnaryOperand(OperationType type, Expression exR, int lineno)
        {
            _type = type;
            _exR = exR;
            Lineno = lineno;
        }

        public override TypeOfValue GetValueType()
        {
            if (_type == OperationType.UnaryMinus)
            {
                TypeOfValue val = _exR.GetValueType();
                if (val == TypeOfValue.int_val || val == TypeOfValue.double_val)
                {
                    return val;
                }
            }

            if (_type == OperationType.BitwiseComplement)
            {
                TypeOfValue val = _exR.GetValueType();
                if (val == TypeOfValue.int_val)
                {
                    return val;
                }
            }

            if (_type == OperationType.LogicalNegation)
            {
                TypeOfValue val = _exR.GetValueType();
                if (val == TypeOfValue.bool_val)
                {
                    return val;
                }
            }

            if (_type == OperationType.IntConversion)
            {
                TypeOfValue val = _exR.GetValueType();
                if (val == TypeOfValue.bool_val || val == TypeOfValue.int_val || val == TypeOfValue.double_val)
                {
                    return TypeOfValue.int_val;
                }
            }

            if (_type == OperationType.DoubleConversion)
            {
                TypeOfValue val = _exR.GetValueType();
                if (val == TypeOfValue.bool_val || val == TypeOfValue.int_val || val == TypeOfValue.double_val)
                {
                    return TypeOfValue.double_val;
                }
            }

            Console.WriteLine("Wrong type in unary expression" + _type.ToString() + "in line: " + Lineno);
            Compiler.typeErrors++;
            return TypeOfValue.wrong_val;
        }
    }

    public class Value : Expression
    {
        public bool _val_bool;
        public int _val_int;
        public double _val_double;
        public string _identificator;

        public Value(bool val_bool, int lineno)
        {
            _val_bool = val_bool;
            Type = TypeOfValue.bool_val;
            Lineno = lineno;
        }

        public Value(int val_int, int lineno)
        {
            _val_int = val_int;
            Type = TypeOfValue.int_val;
            Lineno = lineno;
        }

        public Value(double val_double, int lineno)
        {
            _val_double = val_double;
            Type = TypeOfValue.double_val;
            Lineno = lineno;
        }

        public Value(string identificator, int lineno)
        {
            _identificator = identificator;
            Type = TypeOfValue.identificator;
            Lineno = lineno;
        }

        public override TypeOfValue GetValueType()
        {
            if (Type == TypeOfValue.identificator)
            {
                TypeOfValue identValue;
                if (Compiler.identificatorValueType.TryGetValue(_identificator, out identValue))
                {
                    return identValue;
                }
                else
                {
                    Console.WriteLine("Using uninitialized variable in line: " + Lineno);
                    Compiler.typeErrors++;
                    return TypeOfValue.wrong_val;
                }
            }
            else
            {
                return Type;
            }
        }
    }

    public class Compiler
    {
        public static int errors = 0;
        public static int lines = 1;
        public static List<string> identifiers = new List<string>();
        public static List<string> source;
        private static StreamWriter sw;

        public static Dictionary<string, TypeOfValue> identificatorValueType = new Dictionary<string, TypeOfValue>();
        public static List<string> identificators = new List<string>();
        public static List<(string, int)> identificatorLines = new List<(string, int)>();
        public static List<string> languageKeyWords = new List<string>() { "int", "double", "bool" };
        public static int typeErrors = 0;

        public static int Main(string[] args)
        {
            string file;
            FileStream source;
            Console.WriteLine("\nSingle-Pass CIL Code Generator for Multiline Calculator - Gardens Point");
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
            sw = new StreamWriter(file + ".il");
            GenProlog();
            parser.Parse();

            var x = Parser.program;
            if (x != null)
            {
                if (x.Check())
                {
                    Console.WriteLine("Types OK");
                }
            }

            GenEpilog();
            sw.Close();
            source.Close();
            if (errors == 0)
                Console.WriteLine("  compilation successful\n");
            else
            {
                Console.WriteLine($"\n  {errors} syntax errors detected\n");
                File.Delete(file + ".il");
            }
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

        private static void GenProlog()
        {
            EmitCode(".assembly extern mscorlib { }");
            EmitCode(".assembly calculator { }");
            EmitCode(".method static void main()");
            EmitCode("{");
            EmitCode(".entrypoint");
            EmitCode(".try");
            EmitCode("{");
            EmitCode();

            EmitCode("// prolog");
            EmitCode(".locals init ( float64 temp )");
            for (char c = 'a'; c <= 'z'; ++c)
            {
                EmitCode($".locals init ( int32 _i{c} )");
                EmitCode($".locals init ( float64 _r{c} )");
            }
            EmitCode();
        }

        private static void GenEpilog()
        {
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("catch [mscorlib]System.Exception");
            EmitCode("{");
            EmitCode("callvirt instance string [mscorlib]System.Exception::get_Message()");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
            EmitCode("leave EndMain");
            EmitCode("}");
            EmitCode("EndMain: ret");
            EmitCode("}");
        }

    }

}
