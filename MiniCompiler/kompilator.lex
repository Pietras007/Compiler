%using QUT.Gppg;
%namespace MiniCompiler

IntNumber   	0|([1-9][0-9]*)
DoubleNumber 	0|([1-9][0-9]*)\.[0-9]+
Identificator   [A-Za-z]+[A-Za-z0-9]*
String			\".*\"
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
"true"          { return (int)Tokens.True; }
"false"         { return (int)Tokens.False; }
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
"(int)"       	{ return (int)Tokens.IntConversion; }
"(double)"  	{ return (int)Tokens.DoubleConversion; }
" "       		{ }
<<EOF>>       	{ return (int)Tokens.Eof; }
{IntNumber}		{ yylval.val=yytext; return (int)Tokens.IntNumber; }
{DoubleNumber}	{ yylval.val=yytext; return (int)Tokens.DoubleNumber; }
{Identificator}	{ yylval.val=yytext; return (int)Tokens.Identificator; }
{String}		{ yylval.val=yytext; return (int)Tokens.String; }
{Comment}		{ }
