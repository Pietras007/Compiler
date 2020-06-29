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
        Expression,
        Manystatement
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
        public abstract void GenCode();
    }

    public class Empty_Statement : Statement
    {
        public Empty_Statement()
        {
            Type = StatementType.EmptyStatement;
        }

        public override void GenCode()
        {
            Compiler.EmitCode("nop");
        }

        public override bool Check()
        {
            return true;
        }
    }

    public class ManyStatements : Statement
    {
        public Statement _Lst;
        public Statement _Pst;
        public ManyStatements(Statement Lst, Statement Pst)
        {
            _Lst = Lst;
            _Pst = Pst;
             Type = StatementType.Manystatement;
        }

        public override void GenCode()
        {
            _Lst.GenCode();
            _Pst.GenCode();
        }

        public override bool Check()
        {
            if(_Lst.Check())
            {
                if(_Pst.Check())
                {
                    return true;
                }
            }
            else
            {
                _Pst.Check();
            }

            return false;
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

        public override void GenCode()
        {
            if(_else_st == null)
            {
                _ex.GenCode();
                string label = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                Compiler.EmitCode("brfalse.s " + label);
                Compiler.EmitCode("nop");
                _st.GenCode();
                Compiler.EmitCode("nop");
                Compiler.EmitCode(label + ": nop");
            }
            else
            {
                _ex.GenCode();
                string labelelse = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                string label = "E" + Compiler.Enumber;
                Compiler.Enumber++;

                Compiler.EmitCode("brfalse.s " + labelelse);
                Compiler.EmitCode("nop");
                _st.GenCode();
                Compiler.EmitCode("nop");
                Compiler.EmitCode("br.s " + label);
                Compiler.EmitCode(labelelse + ": nop");
                _else_st.GenCode();
                Compiler.EmitCode("nop");
                Compiler.EmitCode(label + ": nop");
            }
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

        public override void GenCode()
        {
            string whilestart = "E" + Compiler.Enumber;
            Compiler.Enumber++;
            string whilecheck = "E" + Compiler.Enumber;
            Compiler.Enumber++;
            Compiler.EmitCode("br.s  " + whilecheck.ToString());
            Compiler.EmitCode(whilestart.ToString() +": nop");
            _st.GenCode();
            Compiler.EmitCode("nop");
            Compiler.EmitCode(whilecheck.ToString() + ": nop");
            _ex.GenCode();
            Compiler.EmitCode("brtrue.s " + whilestart.ToString());
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

        public Declaration_Statement(TypeOfValue type, Value val_name, Statement st)
        {
            _type = type;
            _val_name = val_name;
            _st = st;
            Type = StatementType.Declaration;
            Compiler.identificators.Add(_val_name._identificator);
            Compiler.identificatorLines.Add(new Tuple<string, int>(_val_name._identificator, _val_name.Lineno));
            if (!Compiler.identificatorValueType.ContainsKey(_val_name._identificator))
            {
                Compiler.identificatorValueType.Add(_val_name._identificator, type);
            }
        }

        public override void GenCode()
        {
            if(_val_name.GetValueType() == TypeOfValue.bool_val)
            {
                Compiler.EmitCode(".locals init (bool _" + _val_name._identificator + "_)");
            }
            else if (_val_name.GetValueType() == TypeOfValue.int_val)
            {
                Compiler.EmitCode(".locals init (int32 _" + _val_name._identificator + "_)");
            }
            else if (_val_name.GetValueType() == TypeOfValue.double_val)
            {
                Compiler.EmitCode(".locals init (float64 _" + _val_name._identificator + "_)");
            }

            if (_st != null)
            {
                _st.GenCode();
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

        public override void GenCode()
        {
            Compiler.EmitCode("br.s Efromreturn");
        }

        public override bool Check()
        {
            return true;
        }
    }

    public class Write_Statement : Statement
    {
        public Expression _ex;
        public string _string;
        public Write_Statement(Expression ex)
        {
            _ex = ex;
            _string = null;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(string _str)
        {
            _string = _str;
            _ex = null;
            Type = StatementType.WriteStatement;
        }

        public override void GenCode()
        {
            if (_ex != null)
            {
                if (_ex.GetValueType() == TypeOfValue.bool_val)
                {
                    _ex.GenCode();
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(bool)");
                    Compiler.EmitCode("nop");
                }
                else if (_ex.GetValueType() == TypeOfValue.int_val)
                {
                    _ex.GenCode();
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(int32)");
                    Compiler.EmitCode("nop");
                }
                else if (_ex.GetValueType() == TypeOfValue.double_val)
                {
                    Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                    Compiler.EmitCode(@"ldstr ""{0:0.000000}""");
                    _ex.GenCode();
                    Compiler.EmitCode("box [mscorlib]System.Double");
                    Compiler.EmitCode("call string [mscorlib]System.String::Format(class [mscorlib]System.IFormatProvider, string, object)");
                    Compiler.EmitCode("call void [mscorlib]System.Console::Write(string)");
                    Compiler.EmitCode("nop");
                }
            }
            else
            {
                Compiler.EmitCode("ldstr " + _string);
                Compiler.EmitCode("call void [mscorlib]System.Console::WriteLine(string)");
                Compiler.EmitCode("nop");
            }
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
                    return false;
                }
            }

            return true;
        }
    }

    public class Read_Statement : Statement
    {
        public Value _ident;
        public Read_Statement(Value ident)
        {
            _ident = ident;
            Type = StatementType.ReadStatement;
        }

        public override void GenCode()
        {
            if(_ident.GetValueType() == TypeOfValue.bool_val)
            {
                Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                Compiler.EmitCode("call bool [mscorlib]System.Boolean::Parse(string)");
                _ident.GenCodeToRead();
            }
            else if(_ident.GetValueType() == TypeOfValue.int_val)
            {
                Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                Compiler.EmitCode("call int32 [mscorlib]System.Int32::Parse(string)");
                _ident.GenCodeToRead();
            }
            else if (_ident.GetValueType() == TypeOfValue.double_val)
            {
                Compiler.EmitCode("call string [mscorlib]System.Console::ReadLine()");
                Compiler.EmitCode("call class [mscorlib]System.Globalization.CultureInfo [mscorlib]System.Globalization.CultureInfo::get_InvariantCulture()");
                Compiler.EmitCode("call float64 [mscorlib]System.Double::Parse(string, class [mscorlib] System.IFormatProvider)");
                _ident.GenCodeToRead();
            }
        }

        public override bool Check()
        {
            if (!Compiler.identificatorValueType.ContainsKey(_ident._identificator))
            {
                Console.WriteLine("Trying to read into uninitialized variable in line: " + _ident.Lineno);
                Compiler.typeErrors++;
                return false;
            }

            return true;
        }
    }

    public class Expression_Statement : Statement
    {
        Expression _ex;

        public Expression_Statement(Expression ex)
        {
            _ex = ex;
            Type = StatementType.Expression;
        }

        public override void GenCode()
        {
            _ex.GenCode();
            Compiler.EmitCode("pop");
        }

        public override bool Check()
        {
            var valType = _ex.GetValueType();
            if (valType != TypeOfValue.wrong_val)
            {
                return true;
            }

            return false;
        }
    }

    public abstract class Expression
    {
        public TypeOfValue Type;
        public abstract TypeOfValue GetValueType();
        public int Lineno;
        public abstract void GenCode();
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

        public override void GenCode()
        {
            if (_type == OperationType.Assign)
            {
                _exR.GenCode();
                Compiler.EmitCode("dup");
                var EXL = (Value)_exL;
                Compiler.EmitCode("stloc _" + EXL._identificator + "_");
            }
            else if (_type == OperationType.BooleanLogicalOr)
            {
                _exL.GenCode();
                _exR.GenCode();
                Compiler.EmitCode("or");
            }
            else if (_type == OperationType.BooleanLogicalAnd)
            {
                _exL.GenCode();
                _exR.GenCode();
                Compiler.EmitCode("and");
            }
            else if (_type == OperationType.Plus)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("add");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("add");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("add");
                }
            }
            else if (_type == OperationType.Minus)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("sub");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("sub");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("sub");
                }
            }
            else if (_type == OperationType.Multiplication)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("mul");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("mul");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("mul");
                }
            }
            else if (_type == OperationType.Divide)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("div");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("div");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("div");
                }
            }
            else if (_type == OperationType.Equal || _type == OperationType.Inequal)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("ceq");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("ceq");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("ceq");
                }

                if (_type == OperationType.Inequal)
                {
                    Compiler.EmitCode("ldc.i4.0");
                    Compiler.EmitCode("ceq");
                }

            }
            else if (_type == OperationType.GreaterThan)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("cgt");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("cgt");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("cgt");
                }
            }
            else if (_type == OperationType.GreaterOrEqual)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("clt.un");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("clt.un");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("clt.un");
                }

                Compiler.EmitCode("ldc.i4.0");
                Compiler.EmitCode("ceq");
            }
            else if (_type == OperationType.LessThan)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("clt");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("clt");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("clt");
                }
            }
            else if (_type == OperationType.LessOrEqual)
            {
                if (_exL.GetValueType() == TypeOfValue.int_val && _exR.GetValueType() == TypeOfValue.double_val)
                {
                    _exL.GenCode();
                    Compiler.EmitCode("conv.r8");
                    _exR.GenCode();
                    Compiler.EmitCode("cgt.un");
                }
                else if (_exL.GetValueType() == TypeOfValue.double_val && _exR.GetValueType() == TypeOfValue.int_val)
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("conv.r8");
                    Compiler.EmitCode("cgt.un");
                }
                else
                {
                    _exL.GenCode();
                    _exR.GenCode();
                    Compiler.EmitCode("cgt.un");
                }
                Compiler.EmitCode("ldc.i4.0");
                Compiler.EmitCode("ceq");
            }
            else if (_type == OperationType.ConditionalOr)
            {
                _exL.GenCode();
                Compiler.EmitCode("ldc.i4.1");
                Compiler.EmitCode("and");
                string orOk = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                Compiler.EmitCode("brtrue.s " + orOk);
                Compiler.EmitCode("ldc.i4.0");
                _exR.GenCode();
                Compiler.EmitCode("or");
                string orAll = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                Compiler.EmitCode("br.s " + orAll);
                Compiler.EmitCode(orOk + ": ldc.i4.1");
                Compiler.EmitCode(orAll + ": nop");
            }
            else if (_type == OperationType.ConditionalAnd)
            {
                _exL.GenCode();
                Compiler.EmitCode("ldc.i4.1");
                Compiler.EmitCode("and");
                string endwrong = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                Compiler.EmitCode("brfalse.s " + endwrong);
                Compiler.EmitCode("ldc.i4.1");
                _exR.GenCode();
                Compiler.EmitCode("and");
                string andAll = "E" + Compiler.Enumber;
                Compiler.Enumber++;
                Compiler.EmitCode("br.s " + andAll);
                Compiler.EmitCode(endwrong + ": ldc.i4.0");
                Compiler.EmitCode(andAll + ": nop");
            }
        }

        public override TypeOfValue GetValueType()
        {
            if (_type == OperationType.Assign)
            {
                TypeOfValue valL = _exL.GetValueType();
                TypeOfValue valR = _exR.GetValueType();
                if (_exL.Type == TypeOfValue.identificator)
                {
                    if (valL == valR)
                    {
                        return valL;
                    }
                    else if(valL == TypeOfValue.double_val && valR == TypeOfValue.int_val)
                    {
                        return TypeOfValue.double_val;
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

        public override void GenCode()
        {
            if (_type == OperationType.UnaryMinus)
            {
                _exR.GenCode();
                Compiler.EmitCode("neg");
            }

            if (_type == OperationType.BitwiseComplement)
            {
                _exR.GenCode();
                Compiler.EmitCode("not");
            }

            if (_type == OperationType.LogicalNegation)
            {
                _exR.GenCode();
                Compiler.EmitCode("ldc.i4.0");
                Compiler.EmitCode("ceq");
            }

            if (_type == OperationType.IntConversion)
            {
                _exR.GenCode();
                Compiler.EmitCode("conv.i4");
            }

            if (_type == OperationType.DoubleConversion)
            {
                _exR.GenCode();
                Compiler.EmitCode("conv.r8");
            }
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

        public void GenCodeToRead()
        {
            if (Type == TypeOfValue.identificator)
            {
                Compiler.EmitCode("stloc _" + _identificator + "_");
            }
        }

        public override void GenCode()
        {
            if(Type == TypeOfValue.identificator)
            {
                Compiler.EmitCode("ldloc _" + _identificator + "_");
            }
            else if(Type == TypeOfValue.bool_val)
            {
                if(_val_bool == false)
                {
                    Compiler.EmitCode("ldc.i4.0");
                }
                else
                {
                    Compiler.EmitCode("ldc.i4.1");
                }
            }
            else if (Type == TypeOfValue.int_val)
            {
                Compiler.EmitCode("ldc.i4.s " + _val_int);
            }
            else if (Type == TypeOfValue.double_val)
            {
                Compiler.EmitCode("ldc.r8 " + _val_double);
            }
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
        public static List<Tuple<string, int>> identificatorLines = new List<Tuple<string, int>>();
        public static List<string> languageKeyWords = new List<string>() { "int", "double", "bool" };
        public static int typeErrors = 0;

        public static int Enumber = 0;

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
                    x.GenCode();
                }
            }

            GenEpilog();
            sw.Close();
            source.Close();
            if (errors == 0 && typeErrors == 0)
            {
                Console.WriteLine("  compilation successful\n");
            }
            else if(errors == 0)
            {
                Console.WriteLine($"\n  {typeErrors} type errors detected\n");
                File.Delete(file + ".il");
            }
            else
            {
                Console.WriteLine($"\n  {errors} syntax errors detected\n");
                File.Delete(file + ".il");
            }
            errors += typeErrors;

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
        }

        private static void GenEpilog()
        {
            EmitCode("Efromreturn: nop");
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
