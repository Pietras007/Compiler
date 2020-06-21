// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace MiniCompiler

%union
{
public string  val;
public char    type;
}

%token Program If Else While Read Write Return Int Double Bool True False Assign ConditionalOr ConditionalAnd BooleanLogicalOr BooleanLogicalAnd Equal Inequal GreaterThan GreaterOrEqual LessThan LessOrEqual Plus Minus Multiplication Divide LogicalNegation BitwiseComplement ParenthesisLeft ParenthesisRight CurlyBracketLeft CurlyBracketRight Semicolon Eof IntConversion DoubleConversion
%token <val> IntNumber DoubleNumber Identificator Comment String
%type <type> prestatement statement expression A B C D E F logical relation oppadd oppmul binary unar

%%

start     : Program CurlyBracketLeft prestatement CurlyBracketRight
          | Program CurlyBracketLeft CurlyBracketRight
          ;

prestatement : Bool Identificator Semicolon
		  | Bool Identificator Semicolon prestatement
		  | Int Identificator Semicolon
		  | Int Identificator Semicolon prestatement
		  | Double Identificator Semicolon
		  | Double Identificator Semicolon prestatement
		  | statement
		  ;

statement : CurlyBracketLeft statement CurlyBracketRight
          | If ParenthesisLeft expression ParenthesisRight statement
		  |	While ParenthesisLeft expression ParenthesisRight statement
		  |	Return Semicolon
		  |	Return Semicolon statement
		  |	expression Semicolon
		  |	expression Semicolon statement
		  | Semicolon
		  | Semicolon statement
		  | Write expression Semicolon
		  | Write expression Semicolon statement
		  | Write String Semicolon
		  | Write String Semicolon statement
		  | Read Identificator Semicolon
		  | Read Identificator Semicolon statement
		  | CurlyBracketLeft CurlyBracketRight
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
		  
C		  : C oppadd D
          | D
          ;
		  
oppadd    : Plus
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