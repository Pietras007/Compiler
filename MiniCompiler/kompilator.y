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
			{ program = new Empty_Statement(); Console.WriteLine("Tree finished"); YYACCEPT; }
          ;

prestatement : Bool Identificator Semicolon
			{ $$ = new Declaration_Statement(ValueType.bool_val, $2); }
		  | Bool Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(ValueType.bool_val, $2, $4); }
		  | Int Identificator Semicolon
			{ $$ = new Declaration_Statement(ValueType.int_val, $2); }
		  | Int Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(ValueType.int_val, $2, $4); }
		  | Double Identificator Semicolon
			{ $$ = new Declaration_Statement(ValueType.double_val, $2); }
		  | Double Identificator Semicolon prestatement
			{ $$ = new Declaration_Statement(ValueType.double_val, $2, $4); }
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
		    { yyerrok(); Console.WriteLine("something missing, line: " + Compiler.lines.ToString()); Compiler.errors++; YYACCEPT; }
          ;
		  
unar      : Minus
			{ $$ = OperationType.Minus; }
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
        DoubleConversion
    }

    public enum ValueType
    {
        bool_val,
        int_val,
        double_val,
        string_val,
        identificator
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
            if (_ex.Check() && _st.Check() && (_else_st == null || _else_st.Check()))
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
            if (_ex.Check() && _st.Check())
            {
                return true;
            }

            return false;
        }
    }

    public class Declaration_Statement : Statement
    {
        public Statement _st;
        public ValueType _type;
        public string _val_name;
        public Declaration_Statement(ValueType type, string val_name)
        {
            _type = type;
            _val_name = val_name;
            Type = StatementType.Declaration;
        }

        public Declaration_Statement(ValueType type, string val_name, Statement st)
        {
            _type = type;
            _val_name = val_name;
            _st = st;
            Type = StatementType.Declaration;
        }

        public override bool Check()
        {
            if ((_st == null || _st.Check()) && !Compiler.identifiers.Contains(_val_name))
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
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(Expression ex, Statement st)
        {
            _st = st;
            _ex = ex;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(string _str)
        {
            _string = _str;
            Type = StatementType.WriteStatement;
        }

        public Write_Statement(string _str, Statement st)
        {
            _st = st;
            _string = _str;
            Type = StatementType.WriteStatement;
        }

        public override bool Check()
        {
            if ((_ex == null || _ex.Check()) && (_st == null || _st.Check()) && _string != null)
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
            if (Compiler.identifiers.Contains(_ident._identificator) && (_st == null || _st.Check()))
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
            if (_ex.Check() && (_st == null || _st.Check()))
            {
                return true;
            }

            return false;
        }
    }

    public abstract class Expression
    {
        public ValueType Type;
        public abstract bool Check();
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

        public override bool Check()
        {
            if (_exL is Value && _exR is Value)
            {
                return true;
            }
            else
            {
                return _exL.Check() && _exR.Check();
            }
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

        public override bool Check()
        {
            if (_exR is Value)
            {
                return true;
            }
            else
            {
                return _exR.Check();
            }
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
            Type = ValueType.bool_val;
        }

        public Value(int val_int)
        {
            _val_int = val_int;
            Type = ValueType.int_val;
        }

        public Value(double val_double)
        {
            _val_double = val_double;
            Type = ValueType.double_val;
        }

        public Value(string identificator)
        {
            _identificator = identificator;
            Type = ValueType.identificator;
        }

        public override bool Check()
        {
            return true;
        }

        public ValueType GetValueType()
        {
            return Type;
        }
    }