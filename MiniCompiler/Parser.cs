// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-F60JC3Q
// DateTime: 6/27/2020 6:24:26 PM
// UserName: User
// Input file <kompilator.y - 6/27/2020 4:28:15 PM>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace MiniCompiler
{
public enum Tokens {error=2,EOF=3,Program=4,If=5,Else=6,
    While=7,Read=8,Write=9,Return=10,Int=11,Double=12,
    Bool=13,True=14,False=15,Assign=16,ConditionalOr=17,ConditionalAnd=18,
    BooleanLogicalOr=19,BooleanLogicalAnd=20,Equal=21,Inequal=22,GreaterThan=23,GreaterOrEqual=24,
    LessThan=25,LessOrEqual=26,Plus=27,Minus=28,Multiplication=29,Divide=30,
    LogicalNegation=31,BitwiseComplement=32,ParenthesisLeft=33,ParenthesisRight=34,CurlyBracketLeft=35,CurlyBracketRight=36,
    Semicolon=37,Eof=38,IntConversion=39,DoubleConversion=40,Identificator=41,Comment=42,
    String=43,BooleanNumber=44,IntNumber=45,DoubleNumber=46};

public struct ValueType
#line 6 "kompilator.y"
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
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[65];
  private static State[] states = new State[105];
  private static string[] nonTerms = new string[] {
      "prestatement", "statement", "expression", "A", "B", "C", "D", "E", "F", 
      "declarationstring", "logical", "relation", "oppadd", "oppmul", "binary", 
      "unar", "start", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{4,3},new int[]{-17,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{35,4});
    states[4] = new State(new int[]{36,8,13,10,11,14,12,18,35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97},new int[]{-1,5,-2,22,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[5] = new State(new int[]{36,6});
    states[6] = new State(new int[]{38,7});
    states[7] = new State(-2);
    states[8] = new State(new int[]{38,9});
    states[9] = new State(-3);
    states[10] = new State(new int[]{41,104},new int[]{-10,11});
    states[11] = new State(new int[]{37,12});
    states[12] = new State(new int[]{13,10,11,14,12,18,35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-4},new int[]{-1,13,-2,22,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[13] = new State(-5);
    states[14] = new State(new int[]{41,104},new int[]{-10,15});
    states[15] = new State(new int[]{37,16});
    states[16] = new State(new int[]{13,10,11,14,12,18,35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-6},new int[]{-1,17,-2,22,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[17] = new State(-7);
    states[18] = new State(new int[]{41,104},new int[]{-10,19});
    states[19] = new State(new int[]{37,20});
    states[20] = new State(new int[]{13,10,11,14,12,18,35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-8},new int[]{-1,21,-2,22,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[21] = new State(-9);
    states[22] = new State(-10);
    states[23] = new State(new int[]{36,26,35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97},new int[]{-2,24,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[24] = new State(new int[]{36,25});
    states[25] = new State(-12);
    states[26] = new State(-25);
    states[27] = new State(new int[]{33,28});
    states[28] = new State(new int[]{41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90},new int[]{-3,29,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[29] = new State(new int[]{34,30});
    states[30] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97},new int[]{-2,31,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[31] = new State(-13);
    states[32] = new State(new int[]{33,33});
    states[33] = new State(new int[]{41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90},new int[]{-3,34,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[34] = new State(new int[]{34,35});
    states[35] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97},new int[]{-2,36,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[36] = new State(-14);
    states[37] = new State(new int[]{37,38});
    states[38] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-15},new int[]{-2,39,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[39] = new State(-16);
    states[40] = new State(new int[]{37,41});
    states[41] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-17},new int[]{-2,42,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[42] = new State(-18);
    states[43] = new State(new int[]{16,44,19,-56,20,-56,29,-56,30,-56,27,-56,28,-56,21,-56,22,-56,23,-56,24,-56,25,-56,26,-56,17,-56,18,-56,37,-56,34,-56});
    states[44] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-4,45,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[45] = new State(new int[]{17,71,18,72,37,-26,34,-26},new int[]{-11,46});
    states[46] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-5,47,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[47] = new State(new int[]{21,74,22,75,23,76,24,77,25,78,26,79,17,-28,18,-28,37,-28,34,-28},new int[]{-12,48});
    states[48] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-6,49,-7,83,-8,86,-9,89,-16,56});
    states[49] = new State(new int[]{27,81,28,82,21,-32,22,-32,23,-32,24,-32,25,-32,26,-32,17,-32,18,-32,37,-32,34,-32},new int[]{-13,50});
    states[50] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-7,51,-8,86,-9,89,-16,56});
    states[51] = new State(new int[]{29,84,30,85,27,-40,28,-40,21,-40,22,-40,23,-40,24,-40,25,-40,26,-40,17,-40,18,-40,37,-40,34,-40},new int[]{-14,52});
    states[52] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-8,53,-9,89,-16,56});
    states[53] = new State(new int[]{19,87,20,88,29,-44,30,-44,27,-44,28,-44,21,-44,22,-44,23,-44,24,-44,25,-44,26,-44,17,-44,18,-44,37,-44,34,-44},new int[]{-15,54});
    states[54] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-9,55,-16,56});
    states[55] = new State(-48);
    states[56] = new State(new int[]{28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,41,66,33,67,2,90},new int[]{-9,57,-16,56});
    states[57] = new State(-52);
    states[58] = new State(-60);
    states[59] = new State(-61);
    states[60] = new State(-62);
    states[61] = new State(-63);
    states[62] = new State(-64);
    states[63] = new State(-53);
    states[64] = new State(-54);
    states[65] = new State(-55);
    states[66] = new State(-56);
    states[67] = new State(new int[]{41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90},new int[]{-3,68,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[68] = new State(new int[]{34,69});
    states[69] = new State(-57);
    states[70] = new State(new int[]{17,71,18,72,37,-27,34,-27},new int[]{-11,46});
    states[71] = new State(-30);
    states[72] = new State(-31);
    states[73] = new State(new int[]{21,74,22,75,23,76,24,77,25,78,26,79,17,-29,18,-29,37,-29,34,-29},new int[]{-12,48});
    states[74] = new State(-34);
    states[75] = new State(-35);
    states[76] = new State(-36);
    states[77] = new State(-37);
    states[78] = new State(-38);
    states[79] = new State(-39);
    states[80] = new State(new int[]{27,81,28,82,21,-33,22,-33,23,-33,24,-33,25,-33,26,-33,17,-33,18,-33,37,-33,34,-33},new int[]{-13,50});
    states[81] = new State(-42);
    states[82] = new State(-43);
    states[83] = new State(new int[]{29,84,30,85,27,-41,28,-41,21,-41,22,-41,23,-41,24,-41,25,-41,26,-41,17,-41,18,-41,37,-41,34,-41},new int[]{-14,52});
    states[84] = new State(-46);
    states[85] = new State(-47);
    states[86] = new State(new int[]{19,87,20,88,29,-45,30,-45,27,-45,28,-45,21,-45,22,-45,23,-45,24,-45,25,-45,26,-45,17,-45,18,-45,37,-45,34,-45},new int[]{-15,54});
    states[87] = new State(-50);
    states[88] = new State(-51);
    states[89] = new State(-49);
    states[90] = new State(new int[]{37,91,38,92});
    states[91] = new State(-58);
    states[92] = new State(-59);
    states[93] = new State(new int[]{43,94,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90},new int[]{-3,101,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[94] = new State(new int[]{37,95});
    states[95] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-19},new int[]{-2,96,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[96] = new State(-20);
    states[97] = new State(new int[]{41,98});
    states[98] = new State(new int[]{37,99});
    states[99] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-23},new int[]{-2,100,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[100] = new State(-24);
    states[101] = new State(new int[]{37,102});
    states[102] = new State(new int[]{35,23,5,27,7,32,10,37,41,43,28,58,32,59,31,60,39,61,40,62,45,63,46,64,44,65,33,67,2,90,9,93,8,97,36,-21},new int[]{-2,103,-3,40,-4,70,-5,73,-6,80,-7,83,-8,86,-9,89,-16,56});
    states[103] = new State(-22);
    states[104] = new State(-11);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-18, new int[]{-17,3});
    rules[2] = new Rule(-17, new int[]{4,35,-1,36,38});
    rules[3] = new Rule(-17, new int[]{4,35,36,38});
    rules[4] = new Rule(-1, new int[]{13,-10,37});
    rules[5] = new Rule(-1, new int[]{13,-10,37,-1});
    rules[6] = new Rule(-1, new int[]{11,-10,37});
    rules[7] = new Rule(-1, new int[]{11,-10,37,-1});
    rules[8] = new Rule(-1, new int[]{12,-10,37});
    rules[9] = new Rule(-1, new int[]{12,-10,37,-1});
    rules[10] = new Rule(-1, new int[]{-2});
    rules[11] = new Rule(-10, new int[]{41});
    rules[12] = new Rule(-2, new int[]{35,-2,36});
    rules[13] = new Rule(-2, new int[]{5,33,-3,34,-2});
    rules[14] = new Rule(-2, new int[]{7,33,-3,34,-2});
    rules[15] = new Rule(-2, new int[]{10,37});
    rules[16] = new Rule(-2, new int[]{10,37,-2});
    rules[17] = new Rule(-2, new int[]{-3,37});
    rules[18] = new Rule(-2, new int[]{-3,37,-2});
    rules[19] = new Rule(-2, new int[]{9,43,37});
    rules[20] = new Rule(-2, new int[]{9,43,37,-2});
    rules[21] = new Rule(-2, new int[]{9,-3,37});
    rules[22] = new Rule(-2, new int[]{9,-3,37,-2});
    rules[23] = new Rule(-2, new int[]{8,41,37});
    rules[24] = new Rule(-2, new int[]{8,41,37,-2});
    rules[25] = new Rule(-2, new int[]{35,36});
    rules[26] = new Rule(-3, new int[]{41,16,-4});
    rules[27] = new Rule(-3, new int[]{-4});
    rules[28] = new Rule(-4, new int[]{-4,-11,-5});
    rules[29] = new Rule(-4, new int[]{-5});
    rules[30] = new Rule(-11, new int[]{17});
    rules[31] = new Rule(-11, new int[]{18});
    rules[32] = new Rule(-5, new int[]{-5,-12,-6});
    rules[33] = new Rule(-5, new int[]{-6});
    rules[34] = new Rule(-12, new int[]{21});
    rules[35] = new Rule(-12, new int[]{22});
    rules[36] = new Rule(-12, new int[]{23});
    rules[37] = new Rule(-12, new int[]{24});
    rules[38] = new Rule(-12, new int[]{25});
    rules[39] = new Rule(-12, new int[]{26});
    rules[40] = new Rule(-6, new int[]{-6,-13,-7});
    rules[41] = new Rule(-6, new int[]{-7});
    rules[42] = new Rule(-13, new int[]{27});
    rules[43] = new Rule(-13, new int[]{28});
    rules[44] = new Rule(-7, new int[]{-7,-14,-8});
    rules[45] = new Rule(-7, new int[]{-8});
    rules[46] = new Rule(-14, new int[]{29});
    rules[47] = new Rule(-14, new int[]{30});
    rules[48] = new Rule(-8, new int[]{-8,-15,-9});
    rules[49] = new Rule(-8, new int[]{-9});
    rules[50] = new Rule(-15, new int[]{19});
    rules[51] = new Rule(-15, new int[]{20});
    rules[52] = new Rule(-9, new int[]{-16,-9});
    rules[53] = new Rule(-9, new int[]{45});
    rules[54] = new Rule(-9, new int[]{46});
    rules[55] = new Rule(-9, new int[]{44});
    rules[56] = new Rule(-9, new int[]{41});
    rules[57] = new Rule(-9, new int[]{33,-3,34});
    rules[58] = new Rule(-9, new int[]{2,37});
    rules[59] = new Rule(-9, new int[]{2,38});
    rules[60] = new Rule(-16, new int[]{28});
    rules[61] = new Rule(-16, new int[]{32});
    rules[62] = new Rule(-16, new int[]{31});
    rules[63] = new Rule(-16, new int[]{39});
    rules[64] = new Rule(-16, new int[]{40});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> Program, CurlyBracketLeft, prestatement, CurlyBracketRight, Eof
#line 31 "kompilator.y"
   { program = ValueStack[ValueStack.Depth-3].stat; Console.WriteLine("Tree finished"); YYAccept(); }
#line default
        break;
      case 3: // start -> Program, CurlyBracketLeft, CurlyBracketRight, Eof
#line 33 "kompilator.y"
   { program = new Empty_Statement(); Console.WriteLine("Tree finished"); YYAbort(); }
#line default
        break;
      case 4: // prestatement -> Bool, declarationstring, Semicolon
#line 37 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.bool_val, ValueStack[ValueStack.Depth-2].value); }
#line default
        break;
      case 5: // prestatement -> Bool, declarationstring, Semicolon, prestatement
#line 39 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.bool_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 6: // prestatement -> Int, declarationstring, Semicolon
#line 41 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.int_val, ValueStack[ValueStack.Depth-2].value); }
#line default
        break;
      case 7: // prestatement -> Int, declarationstring, Semicolon, prestatement
#line 43 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.int_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 8: // prestatement -> Double, declarationstring, Semicolon
#line 45 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.double_val, ValueStack[ValueStack.Depth-2].value); }
#line default
        break;
      case 9: // prestatement -> Double, declarationstring, Semicolon, prestatement
#line 47 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.double_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 10: // prestatement -> statement
#line 49 "kompilator.y"
   { CurrentSemanticValue.stat = ValueStack[ValueStack.Depth-1].stat; }
#line default
        break;
      case 11: // declarationstring -> Identificator
#line 52 "kompilator.y"
                                  { CurrentSemanticValue.value = new Value(ValueStack[ValueStack.Depth-1].val, Compiler.lines); }
#line default
        break;
      case 12: // statement -> CurlyBracketLeft, statement, CurlyBracketRight
#line 56 "kompilator.y"
   { CurrentSemanticValue.stat = ValueStack[ValueStack.Depth-2].stat; }
#line default
        break;
      case 13: // statement -> If, ParenthesisLeft, expression, ParenthesisRight, statement
#line 58 "kompilator.y"
   { CurrentSemanticValue.stat = new If_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 14: // statement -> While, ParenthesisLeft, expression, ParenthesisRight, statement
#line 60 "kompilator.y"
   { CurrentSemanticValue.stat = new While_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 15: // statement -> Return, Semicolon
#line 62 "kompilator.y"
   { CurrentSemanticValue.stat = new Return_Statement(); }
#line default
        break;
      case 16: // statement -> Return, Semicolon, statement
#line 64 "kompilator.y"
   { CurrentSemanticValue.stat = new Return_Statement(); }
#line default
        break;
      case 17: // statement -> expression, Semicolon
#line 66 "kompilator.y"
   { CurrentSemanticValue.stat = new Expression_Statement(ValueStack[ValueStack.Depth-2].expression); }
#line default
        break;
      case 18: // statement -> expression, Semicolon, statement
#line 68 "kompilator.y"
   { CurrentSemanticValue.stat = new Expression_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 19: // statement -> Write, String, Semicolon
#line 70 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-2].val); }
#line default
        break;
      case 20: // statement -> Write, String, Semicolon, statement
#line 72 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-3].val, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 21: // statement -> Write, expression, Semicolon
#line 74 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-2].expression); }
#line default
        break;
      case 22: // statement -> Write, expression, Semicolon, statement
#line 76 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 23: // statement -> Read, Identificator, Semicolon
#line 78 "kompilator.y"
   { CurrentSemanticValue.stat = new Read_Statement(new Value(ValueStack[ValueStack.Depth-2].val, Compiler.lines)); }
#line default
        break;
      case 24: // statement -> Read, Identificator, Semicolon, statement
#line 80 "kompilator.y"
   { CurrentSemanticValue.stat = new Read_Statement(new Value(ValueStack[ValueStack.Depth-3].val, Compiler.lines), ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 25: // statement -> CurlyBracketLeft, CurlyBracketRight
#line 82 "kompilator.y"
   { CurrentSemanticValue.stat = new Empty_Statement(); }
#line default
        break;
      case 26: // expression -> Identificator, Assign, A
#line 86 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(new Value(ValueStack[ValueStack.Depth-3].val, Compiler.lines), OperationType.Assign, ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 27: // expression -> A
#line 88 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 28: // A -> A, logical, B
#line 92 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 29: // A -> B
#line 94 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 30: // logical -> ConditionalOr
#line 98 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.ConditionalOr; }
#line default
        break;
      case 31: // logical -> ConditionalAnd
#line 100 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.ConditionalAnd; }
#line default
        break;
      case 32: // B -> B, relation, C
#line 104 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 33: // B -> C
#line 106 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 34: // relation -> Equal
#line 110 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Equal; }
#line default
        break;
      case 35: // relation -> Inequal
#line 112 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Inequal; }
#line default
        break;
      case 36: // relation -> GreaterThan
#line 114 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.GreaterThan; }
#line default
        break;
      case 37: // relation -> GreaterOrEqual
#line 116 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.GreaterOrEqual; }
#line default
        break;
      case 38: // relation -> LessThan
#line 118 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LessThan; }
#line default
        break;
      case 39: // relation -> LessOrEqual
#line 120 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LessOrEqual; }
#line default
        break;
      case 40: // C -> C, oppadd, D
#line 124 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 41: // C -> D
#line 126 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 42: // oppadd -> Plus
#line 130 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Plus; }
#line default
        break;
      case 43: // oppadd -> Minus
#line 132 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Minus; }
#line default
        break;
      case 44: // D -> D, oppmul, E
#line 136 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 45: // D -> E
#line 138 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 46: // oppmul -> Multiplication
#line 142 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Multiplication; }
#line default
        break;
      case 47: // oppmul -> Divide
#line 144 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Divide; }
#line default
        break;
      case 48: // E -> E, binary, F
#line 148 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 49: // E -> F
#line 150 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 50: // binary -> BooleanLogicalOr
#line 154 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BooleanLogicalOr; }
#line default
        break;
      case 51: // binary -> BooleanLogicalAnd
#line 156 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BooleanLogicalAnd; }
#line default
        break;
      case 52: // F -> unar, F
#line 160 "kompilator.y"
   { CurrentSemanticValue.expression = new UnaryOperand(ValueStack[ValueStack.Depth-2].opptype, ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 53: // F -> IntNumber
#line 162 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].i_val, Compiler.lines); }
#line default
        break;
      case 54: // F -> DoubleNumber
#line 164 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].d_val, Compiler.lines); }
#line default
        break;
      case 55: // F -> BooleanNumber
#line 166 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].b_val, Compiler.lines); }
#line default
        break;
      case 56: // F -> Identificator
#line 168 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].val, Compiler.lines);}
#line default
        break;
      case 57: // F -> ParenthesisLeft, expression, ParenthesisRight
#line 170 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-2].expression; }
#line default
        break;
      case 58: // F -> error, Semicolon
#line 172 "kompilator.y"
      { yyerrok(); Console.WriteLine("error syntax, line: " + Compiler.lines.ToString()); Compiler.errors++;  }
#line default
        break;
      case 59: // F -> error, Eof
#line 174 "kompilator.y"
      { yyerrok(); Console.WriteLine("something missing, line: " + Compiler.lines.ToString()); Compiler.errors++; YYAbort(); }
#line default
        break;
      case 60: // unar -> Minus
#line 178 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.UnaryMinus; }
#line default
        break;
      case 61: // unar -> BitwiseComplement
#line 180 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BitwiseComplement; }
#line default
        break;
      case 62: // unar -> LogicalNegation
#line 182 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LogicalNegation; }
#line default
        break;
      case 63: // unar -> IntConversion
#line 184 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.IntConversion; }
#line default
        break;
      case 64: // unar -> DoubleConversion
#line 186 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.DoubleConversion; }
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 191 "kompilator.y"

public static Statement program;

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
