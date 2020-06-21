// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace MiniCompiler

%union
{
public string  val;
public char    type;
}

%token Program If Else While Read Write Return Int Double Bool True False Assign ConditionalOr ConditionalAnd BooleanLogicalOr BooleanLogicalAnd Equal Inequal GreaterThan GreaterOrEqual LessThan LessOrEqual Plus Minus Multiplication Divide LogicalNegation BitwiseComplement ParenthesisLeft ParenthesisRight CurlyBracketLeft CurlyBracketRight Semicolon Eof
%token <val> IntNumber DoubleNumber Identificator Comment String
%type <type> statement expression A B C D E F logical relation oppadd oppmul binary unar

%%

start     : Program CurlyBracketLeft statement CurlyBracketRight
          | Program CurlyBracketLeft CurlyBracketRight
          ;

statement : CurlyBracketLeft statement CurlyBracketRight
          | If ParenthesisLeft expression ParenthesisRight statement
		  |	While ParenthesisLeft expression ParenthesisRight statement
		  |	Return Semicolon
		  |	expression Semicolon
		  | Semicolon
		  | Bool Identificator Semicolon
		  | Int Identificator Semicolon
		  | Double Identificator Semicolon
		  | Write expression Semicolon
		  | Write String Semicolon
		  | Read Identificator Semicolon
		  | statement statement
          ;

expression : Identificator Assign A
          | A
          ;

A         : A logical B
          | B
          ;
		  
logical   : ConditionalOr
          | ConditionalAnd
          ;

B		  : B relation C
          | C
          ;

relation  : Equal
          | Inequal
		  | GreaterThan
		  | GreaterOrEqual 
		  | LessThan
		  | LessOrEqual
          ;
		  
C		  : C opadd D
          | D
          ;
		  
opadd     : Plus
          | Minus
          ;
		  
D		  : D oppmul E
          | E
          ;
		  
oppmul    : Multiplication
          | Divide
          ;
		  
E		  : E binary F
          | F
          ;

binary    : BooleanLogicalOr
          | BooleanLogicalAnd
          ;
		  
F		  : unar F
          | Identificator
		  | IntNumber
		  |	DoubleNumber
		  | ParenthesisLeft expression ParenthesisRight
          ;
		  
unar      : Minus
          | BitwiseComplement
		  | LogicalNegation
		  |	IntConversion
		  |	DoubleConversion
          ;
%%