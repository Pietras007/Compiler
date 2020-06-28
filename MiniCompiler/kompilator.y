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
	public Value value; //Value
	public OperationType opptype; //OperationType
}

%token Program If Else While Read Write Return Int Double Bool True False Assign ConditionalOr ConditionalAnd BooleanLogicalOr BooleanLogicalAnd Equal Inequal GreaterThan GreaterOrEqual LessThan LessOrEqual Plus Minus Multiplication Divide LogicalNegation BitwiseComplement ParenthesisLeft ParenthesisRight CurlyBracketLeft CurlyBracketRight Semicolon Eof IntConversion DoubleConversion
%token <val> Identificator Comment String
%token <b_val> BooleanNumber
%token <i_val> IntNumber
%token <d_val> DoubleNumber
%type <stat> prestatement manystatements statement
%type <expression> expression A B C D E F
%type <value> declarationstring
%type <opptype> logical relation oppadd oppmul binary unar

%%

start     : Program CurlyBracketLeft prestatement CurlyBracketRight Eof
			{ program = $3; Console.WriteLine("Tree finished"); YYACCEPT; }
          ;

prestatement : Bool declarationstring Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.bool_val, $2, $4); }
		  | Int declarationstring Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.int_val, $2, $4); }
		  | Double declarationstring Semicolon prestatement
			{ $$ = new Declaration_Statement(TypeOfValue.double_val, $2, $4); }
		  | manystatements
			{ $$ = $1; }
		  ;

declarationstring : Identificator { $$ = new Value($1, Compiler.lines); }
		  ;

manystatements: 
		    { $$ = new Empty_Statement(); }
		  | statement manystatements
			{ $$ = new ManyStatements($1, $2); }
		  ;

statement : CurlyBracketLeft manystatements CurlyBracketRight
			{ $$ = $2; }
          | If ParenthesisLeft expression ParenthesisRight statement
			{ $$ = new If_Statement($3, $5); }
		  | If ParenthesisLeft expression ParenthesisRight statement Else statement
			{ $$ = new If_Statement($3, $5, $7); }
		  |	While ParenthesisLeft expression ParenthesisRight statement
			{ $$ = new While_Statement($3, $5); }
		  |	Return Semicolon
			{ $$ = new Return_Statement(); }
		  |	expression Semicolon
			{ $$ = new Expression_Statement($1); }
		  | Write String Semicolon
			{ $$ = new Write_Statement($2); }
		  | Write expression Semicolon
			{ $$ = new Write_Statement($2); }
		  | Read Identificator Semicolon
			{ $$ = new Read_Statement(new Value($2, Compiler.lines)); }
          ;

expression : Identificator Assign A
			{ $$ = new Operand(new Value($1, Compiler.lines), OperationType.Assign, $3, Compiler.lines); }
          | A
			{ $$ = $1; }
          ;

A         : A logical B
			{ $$ = new Operand($1, $2 ,$3, Compiler.lines); }
          | B
			{ $$ = $1; }
          ;
		  
logical   : ConditionalOr
			{ $$ = OperationType.ConditionalOr; }
          | ConditionalAnd
			{ $$ = OperationType.ConditionalAnd; }
          ;

B		  : B relation C
			{ $$ = new Operand($1, $2 ,$3, Compiler.lines); }
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
			{ $$ = new Operand($1, $2 ,$3, Compiler.lines); }
          | D
			{ $$ = $1; }
          ;
		  
oppadd    : Plus
			{ $$ = OperationType.Plus; }
          | Minus
			{ $$ = OperationType.Minus; }
          ;
		  
D		  : D oppmul E
			{ $$ = new Operand($1, $2 ,$3, Compiler.lines); }
          | E
			{ $$ = $1; }
          ;
		  
oppmul    : Multiplication
			{ $$ = OperationType.Multiplication; }
          | Divide
			{ $$ = OperationType.Divide; }
          ;
		  
E		  : E binary F
			{ $$ = new Operand($1, $2 ,$3, Compiler.lines); }
          | F
			{ $$ = $1; }
          ;

binary    : BooleanLogicalOr
			{ $$ = OperationType.BooleanLogicalOr; }
          | BooleanLogicalAnd
			{ $$ = OperationType.BooleanLogicalAnd; }
          ;
		  
F		  : unar F
			{ $$ = new UnaryOperand($1, $2, Compiler.lines); }
		  | IntNumber
			{ $$ = new Value($1, Compiler.lines); }
		  |	DoubleNumber
			{ $$ = new Value($1, Compiler.lines); }
		  | BooleanNumber
			{ $$ = new Value($1, Compiler.lines); }
          | Identificator
			{ $$ = new Value($1, Compiler.lines);}
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