// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace MiniCompiler

%union
{
	public string val;
	public char type;
	public bool b_val;
	public int i_val;
	public double d_val;
	public Parser.Statement stat; //Statement
	public Parser.Expression expression; //Expression
	public Parser.OperationType opptype; //OperationType
}

%token Program If Else While Read Write Return Int Double Bool True False Assign ConditionalOr ConditionalAnd BooleanLogicalOr BooleanLogicalAnd Equal Inequal GreaterThan GreaterOrEqual LessThan LessOrEqual Plus Minus Multiplication Divide LogicalNegation BitwiseComplement ParenthesisLeft ParenthesisRight CurlyBracketLeft CurlyBracketRight Semicolon Eof IntConversion DoubleConversion
%token <val> Identificator Comment String
%token <b_val> BooleanNumber
%token <i_val> IntNumber
%token <d_val> DoubleNumber
%type <stat> prestatement statement
%type <expression> expression A B C D E F
%type <opptype> logical relation oppadd oppmul binary unar

%%

start     : Program CurlyBracketLeft prestatement CurlyBracketRight Eof
			{ program = $3; Console.WriteLine("Tree finished"); YYACCEPT; }
          | Program CurlyBracketLeft CurlyBracketRight Eof
			{ program = new Empty_Statement(); Console.WriteLine("Tree finished"); YYABORT; }
          ;

prestatement : Bool Identificator Semicolon
			{ $$ = new Declaration_Statement(TypeOfValue.bool_val, $2); }
		  | Bool Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.bool_val, $2, $4); }
		  | Int Identificator Semicolon
			{ $$ = new Declaration_Statement(TypeOfValue.int_val, $2); }
		  | Int Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.int_val, $2, $4); }
		  | Double Identificator Semicolon
			{ $$ = new Declaration_Statement(TypeOfValue.double_val, $2); }
		  | Double Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.double_val, $2, $4); }
		  | statement
			{ $$ = $1; }
		  ;

statement : CurlyBracketLeft statement CurlyBracketRight
			{ $$ = $2; }
          | If ParenthesisLeft expression ParenthesisRight statement
			{ $$ = new If_Statement($3, $5); }
		  |	While ParenthesisLeft expression ParenthesisRight statement
			{ $$ = new While_Statement($3, $5); }
		  |	Return Semicolon
			{ $$ = new Return_Statement(); }
		  |	Return Semicolon statement
			{ $$ = new Return_Statement(); }
		  |	expression Semicolon
			{ $$ = new Expression_Statement($1); }
		  |	expression Semicolon statement
			{ $$ = new Expression_Statement($1, $3); }

		  | Write expression Semicolon
			{ $$ = new Write_Statement($2); }
		  | Write expression Semicolon statement
			{ $$ = new Write_Statement($2, $4); }
		  | Write String Semicolon
			{ $$ = new Write_Statement(new Value($2)); }
		  | Write String Semicolon statement
			{ $$ = new Write_Statement(new Value($2), $4); }
		  | Read Identificator Semicolon
			{ $$ = new Read_Statement(new Value($2)); }
		  | Read Identificator Semicolon statement
			{ $$ = new Read_Statement(new Value($2), $4); }
		  | CurlyBracketLeft CurlyBracketRight
			{ $$ = new Empty_Statement(); }
          ;

expression : Identificator Assign A
			{ $$ = new Operand(new Value($1), OperationType.Assign, $3); }
          | A
			{ $$ = $1; }
          ;

A         : A logical B
			{ $$ = new Operand($1, $2 ,$3); }
          | B
			{ $$ = $1; }
          ;
		  
logical   : ConditionalOr
			{ $$ = OperationType.ConditionalOr; }
          | ConditionalAnd
			{ $$ = OperationType.ConditionalAnd; }
          ;

B		  : B relation C
			{ $$ = new Operand($1, $2 ,$3); }
          | C
			{ $$ = $1; }
          ;

relation  : Equal
			{ $$ = OperationType.Equal; }
          | Inequal
			{ $$ = OperationType.Inequal; }
		  | GreaterThan
			{ $$ = OperationType.GreaterThan; }
		  | GreaterOrEqual 
			{ $$ = OperationType.GreaterOrEqual; }
		  | LessThan
			{ $$ = OperationType.LessThan; }
		  | LessOrEqual
			{ $$ = OperationType.LessOrEqual; }
          ;
		  
C		  : C oppadd D
			{ $$ = new Operand($1, $2 ,$3); }
          | D
			{ $$ = $1; }
          ;
		  
oppadd    : Plus
			{ $$ = OperationType.Plus; }
          | Minus
			{ $$ = OperationType.Minus; }
          ;
		  
D		  : D oppmul E
			{ $$ = new Operand($1, $2 ,$3); }
          | E
			{ $$ = $1; }
          ;
		  
oppmul    : Multiplication
			{ $$ = OperationType.Multiplication; }
          | Divide
			{ $$ = OperationType.Divide; }
          ;
		  
E		  : E binary F
			{ $$ = new Operand($1, $2 ,$3); }
          | F
			{ $$ = $1; }
          ;

binary    : BooleanLogicalOr
			{ $$ = OperationType.BooleanLogicalOr; }
          | BooleanLogicalAnd
			{ $$ = OperationType.BooleanLogicalAnd; }
          ;
		  
F		  : unar F
			{ $$ = new UnaryOperand($1, $2); }
		  | IntNumber
			{ $$ = new Value($1); }
		  |	DoubleNumber
			{ $$ = new Value($1); }
		  | BooleanNumber
			{ $$ = new Value($1); }
          | Identificator
			{ $$ = new Value($1);}
		  | ParenthesisLeft expression ParenthesisRight
			{ $$ = $2; }
		  | error Semicolon
		    { yyerrok(); Console.WriteLine("error syntax, line: " + Compiler.lines.ToString()); Compiler.errors++;  }
		  | error Eof
		    { yyerrok(); Console.WriteLine("something missing, line: " + Compiler.lines.ToString()); Compiler.errors++; YYABORT; }
          ;
		  
unar      : Minus
			{ $$ = OperationType.UnaryMinus; }
          | BitwiseComplement
			{ $$ = OperationType.BitwiseComplement; }
		  | LogicalNegation
			{ $$ = OperationType.LogicalNegation; }
		  |	IntConversion
			{ $$ = OperationType.IntConversion; }
		  |	DoubleConversion
			{ $$ = OperationType.DoubleConversion; }
          ;


%%

public static Statement program;

public Parser(Scanner scanner) : base(scanner) { }

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
            if(_ex.GetValueType() != TypeOfValue.bool_val)
            {
                Console.WriteLine("Error in expression (If_Statement)");
                Compiler.typeErrors++;
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
                Console.WriteLine("Error in expression (While_Statement)");
                Compiler.typeErrors++;
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
        public string _val_name;
        public Declaration_Statement(TypeOfValue type, string val_name)
        {
            _type = type;
            _val_name = val_name;
            _st = null;
            Type = StatementType.Declaration;
            Compiler.identificators.Add(val_name);
            if (!Compiler.identificatorValueType.ContainsKey(val_name))
            {
                Compiler.identificatorValueType.Add(val_name, type);
            }
        }

        public Declaration_Statement(TypeOfValue type, string val_name, Statement st)
        {
            _type = type;
            _val_name = val_name;
            _st = st;
            Type = StatementType.Declaration;
            Compiler.identificators.Add(val_name);
            if (!Compiler.identificatorValueType.ContainsKey(val_name))
            {
                Compiler.identificatorValueType.Add(val_name, type);
            }
        }

        public override bool Check()
        {
            if(Compiler.languageKeyWords.Contains(_val_name)) //TODO: check if not "kwy" word
            {
                Console.WriteLine("Using 'language key word'.");
                Compiler.typeErrors++;
                return false;
            }

            int amount = 0;
            for(int i=0;i< Compiler.identificators.Count;i++)
            {
                if(Compiler.identificators[i] == _val_name)
                {
                    amount++;
                }
            }

            if (amount > 1)
            {
                Console.WriteLine("Such declaration already exists.");
                Compiler.typeErrors++;
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
            if(_string == null && _ex.GetValueType() == TypeOfValue.wrong_val || _ex.GetValueType() == TypeOfValue.assignment)
            {
                Console.WriteLine("Wrong type in write expression");
                Compiler.typeErrors++;
                return false;
            }

            if (_string != null && (_st == null || _st.Check()))
            {
                return true;
            }

            if (_string == null && (_st == null || _st.Check()) && _string != null)
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
            if(!Compiler.identificatorValueType.ContainsKey(_ident._identificator))
            {
                Console.WriteLine("Trying to read into uninitialized variable");
                Compiler.typeErrors++;
                return false;
            }
            else if(_st == null || _st.Check())
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
            if (_ex.GetValueType() != TypeOfValue.wrong_val && (_st == null || _st.Check()))
            {
                return true;
            }

            //Console.WriteLine("Error in expression (Expression_Statement)");
            //Console.WriteLine(_ex.Type.ToString());
            //Console.WriteLine(_ex.GetValueType().ToString());
            return false;
        }
    }

    public abstract class Expression
    {
        public TypeOfValue Type;
        public abstract TypeOfValue GetValueType();
    }


    public class Operand : Expression
    {
        public OperationType _type;
        public Expression _exL;
        public Expression _exR;

        public Operand(Expression exL, OperationType type, Expression exR)
        {
            _exL = exL;
            _exR = exR;
            _type = type;
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
                else if((valL == TypeOfValue.int_val && valR == TypeOfValue.double_val) || (valL == TypeOfValue.double_val && valR == TypeOfValue.int_val) || (valL == TypeOfValue.double_val && valR == TypeOfValue.double_val))
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
                if(valL == TypeOfValue.bool_val && valR == TypeOfValue.bool_val)
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

            Console.WriteLine("Wrong type in operation expression" + _type.ToString());
            Compiler.typeErrors++;
            return TypeOfValue.wrong_val;
        }
    }

    public class UnaryOperand : Expression
    {
        public OperationType _type;
        public Expression _exR;

        public UnaryOperand(OperationType type, Expression exR)
        {
            _type = type;
            _exR = exR;
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

            Console.WriteLine("Wrong type in unary expression" + _type.ToString());
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

        public Value(bool val_bool)
        {
            _val_bool = val_bool;
            Type = TypeOfValue.bool_val;
        }

        public Value(int val_int)
        {
            _val_int = val_int;
            Type = TypeOfValue.int_val;
        }

        public Value(double val_double)
        {
            _val_double = val_double;
            Type = TypeOfValue.double_val;
        }

        public Value(string identificator)
        {
            _identificator = identificator;
            Type = TypeOfValue.identificator;
        }

        public override TypeOfValue GetValueType()
        {
            if (Type == TypeOfValue.identificator)
            {
                TypeOfValue identValue;
                if(Compiler.identificatorValueType.TryGetValue(_identificator, out identValue))
                {
                    return identValue;
                }
                else
                {
                    Console.WriteLine("Using uninitialized variable");
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