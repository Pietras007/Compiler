%using QUT.Gppg;
%namespace MiniCompiler

BooleanNumber	"true"|"false"
IntNumber   	0|([1-9][0-9]*)
DoubleNumber 	(0|([1-9][0-9]*))\.[0-9]+
Identificator   [a-zA-Z]+[a-zA-Z0-9]*
String			\"([^\\\"\n]|\\.)*\"
Comment			"//".*


%%

"program"       { return (int)Tokens.Program; }
"if"        	{ return (int)Tokens.If; }
"else"   		{ return (int)Tokens.Else; }
"while"  		{ return (int)Tokens.While; }
"read"       	{ return (int)Tokens.Read; }
"write"         { return (int)Tokens.Write; }
"return"        { return (int)Tokens.Return; }
"int"			{ return (int)Tokens.Int; }
"double"        { return (int)Tokens.Double; }
"bool"          { return (int)Tokens.Bool; }
"="           	{ return (int)Tokens.Assign; }
"||"       		{ return (int)Tokens.ConditionalOr; }
"&&"      		{ return (int)Tokens.ConditionalAnd; }
"|"          	{ return (int)Tokens.BooleanLogicalOr; }
"&"          	{ return (int)Tokens.BooleanLogicalAnd; }
"=="    		{ return (int)Tokens.Equal; }
"!="            { return (int)Tokens.Inequal; }
">"				{ return (int)Tokens.GreaterThan; }
">="        	{ return (int)Tokens.GreaterOrEqual; }
"<"          	{ return (int)Tokens.LessThan; }
"<="          	{ return (int)Tokens.LessOrEqual; }
"+"         	{ return (int)Tokens.Plus; }
"-"           	{ return (int)Tokens.Minus; }
"*"       		{ return (int)Tokens.Multiplication; }
"/"       		{ return (int)Tokens.Divide; }
"!"       		{ return (int)Tokens.LogicalNegation; }
"~"       		{ return (int)Tokens.BitwiseComplement; }
"("       		{ return (int)Tokens.ParenthesisLeft; }
")"       		{ return (int)Tokens.ParenthesisRight; }
"{"       		{ return (int)Tokens.CurlyBracketLeft; }
"}"       		{ return (int)Tokens.CurlyBracketRight; }
";"       		{ return (int)Tokens.Semicolon; }
" "       		{ }
"\t"       		{ }
"\r"       		{ }
<<EOF>>       	{ return (int)Tokens.Eof; }
{IntNumber}		{ yylval.i_val=Int32.Parse(yytext); return (int)Tokens.IntNumber; }
{DoubleNumber}	{ yylval.d_val=Double.Parse(yytext, System.Globalization.CultureInfo.InvariantCulture); return (int)Tokens.DoubleNumber; }
{BooleanNumber}	{ yylval.b_val=Boolean.Parse(yytext); return (int)Tokens.BooleanNumber; }
{Identificator}	{ yylval.val=yytext; return (int)Tokens.Identificator; }
{String}		{ yylval.val=yytext; return (int)Tokens.String; }
{Comment}		{ }
"\n"			{ Compiler.lines++; }
.				{ return (int)Tokens.Error; }