// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace MiniCompiler

%union
{
	public string val;
	public char type;
	public bool b_val;
	public int i_val;
	public double d_val;
	public Statement stat; //Statement
	public Expression expression; //Expression
	public OperationType opptype; //OperationType
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