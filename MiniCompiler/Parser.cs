// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-F60JC3Q
// DateTime: 6/28/2020 6:47:52 PM
// UserName: User
// Input file <kompilator.y - 6/28/2020 6:31:45 PM>

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
  private static Rule[] rules = new Rule[58];
  private static State[] states = new State[101];
  private static string[] nonTerms = new string[] {
      "prestatement", "manystatements", "statement", "expression", "A", "B", 
      "C", "D", "E", "F", "declarationstring", "logical", "relation", "oppadd", 
      "oppmul", "binary", "unar", "start", "$accept", };

  static Parser() {
    states[0] = new State(new int[]{4,3},new int[]{-18,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{35,4});
    states[4] = new State(new int[]{13,8,11,12,12,16,35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-1,5,-2,20,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[5] = new State(new int[]{36,6});
    states[6] = new State(new int[]{38,7});
    states[7] = new State(-2);
    states[8] = new State(new int[]{41,100},new int[]{-11,9});
    states[9] = new State(new int[]{37,10});
    states[10] = new State(new int[]{13,8,11,12,12,16,35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-1,11,-2,20,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[11] = new State(-3);
    states[12] = new State(new int[]{41,100},new int[]{-11,13});
    states[13] = new State(new int[]{37,14});
    states[14] = new State(new int[]{13,8,11,12,12,16,35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-1,15,-2,20,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[15] = new State(-4);
    states[16] = new State(new int[]{41,100},new int[]{-11,17});
    states[17] = new State(new int[]{37,18});
    states[18] = new State(new int[]{13,8,11,12,12,16,35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-1,19,-2,20,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[19] = new State(-5);
    states[20] = new State(-6);
    states[21] = new State(new int[]{35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-2,22,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[22] = new State(-9);
    states[23] = new State(new int[]{35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97,36,-8},new int[]{-2,24,-3,21,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[24] = new State(new int[]{36,25});
    states[25] = new State(-10);
    states[26] = new State(new int[]{33,27});
    states[27] = new State(new int[]{41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89},new int[]{-4,28,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[28] = new State(new int[]{34,29});
    states[29] = new State(new int[]{35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97},new int[]{-3,30,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[30] = new State(new int[]{6,31,35,-11,5,-11,7,-11,10,-11,41,-11,28,-11,32,-11,31,-11,39,-11,40,-11,45,-11,46,-11,44,-11,33,-11,2,-11,9,-11,8,-11,36,-11});
    states[31] = new State(new int[]{35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97},new int[]{-3,32,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[32] = new State(-12);
    states[33] = new State(new int[]{33,34});
    states[34] = new State(new int[]{41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89},new int[]{-4,35,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[35] = new State(new int[]{34,36});
    states[36] = new State(new int[]{35,23,5,26,7,33,10,38,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89,9,92,8,97},new int[]{-3,37,-4,40,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[37] = new State(-13);
    states[38] = new State(new int[]{37,39});
    states[39] = new State(-14);
    states[40] = new State(new int[]{37,41});
    states[41] = new State(-15);
    states[42] = new State(new int[]{16,43,19,-49,20,-49,29,-49,30,-49,27,-49,28,-49,21,-49,22,-49,23,-49,24,-49,25,-49,26,-49,17,-49,18,-49,37,-49,34,-49});
    states[43] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-5,44,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[44] = new State(new int[]{17,70,18,71,37,-19,34,-19},new int[]{-12,45});
    states[45] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-6,46,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[46] = new State(new int[]{21,73,22,74,23,75,24,76,25,77,26,78,17,-21,18,-21,37,-21,34,-21},new int[]{-13,47});
    states[47] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-7,48,-8,82,-9,85,-10,88,-17,55});
    states[48] = new State(new int[]{27,80,28,81,21,-25,22,-25,23,-25,24,-25,25,-25,26,-25,17,-25,18,-25,37,-25,34,-25},new int[]{-14,49});
    states[49] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-8,50,-9,85,-10,88,-17,55});
    states[50] = new State(new int[]{29,83,30,84,27,-33,28,-33,21,-33,22,-33,23,-33,24,-33,25,-33,26,-33,17,-33,18,-33,37,-33,34,-33},new int[]{-15,51});
    states[51] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-9,52,-10,88,-17,55});
    states[52] = new State(new int[]{19,86,20,87,29,-37,30,-37,27,-37,28,-37,21,-37,22,-37,23,-37,24,-37,25,-37,26,-37,17,-37,18,-37,37,-37,34,-37},new int[]{-16,53});
    states[53] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-10,54,-17,55});
    states[54] = new State(-41);
    states[55] = new State(new int[]{28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,41,65,33,66,2,89},new int[]{-10,56,-17,55});
    states[56] = new State(-45);
    states[57] = new State(-53);
    states[58] = new State(-54);
    states[59] = new State(-55);
    states[60] = new State(-56);
    states[61] = new State(-57);
    states[62] = new State(-46);
    states[63] = new State(-47);
    states[64] = new State(-48);
    states[65] = new State(-49);
    states[66] = new State(new int[]{41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89},new int[]{-4,67,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[67] = new State(new int[]{34,68});
    states[68] = new State(-50);
    states[69] = new State(new int[]{17,70,18,71,37,-20,34,-20},new int[]{-12,45});
    states[70] = new State(-23);
    states[71] = new State(-24);
    states[72] = new State(new int[]{21,73,22,74,23,75,24,76,25,77,26,78,17,-22,18,-22,37,-22,34,-22},new int[]{-13,47});
    states[73] = new State(-27);
    states[74] = new State(-28);
    states[75] = new State(-29);
    states[76] = new State(-30);
    states[77] = new State(-31);
    states[78] = new State(-32);
    states[79] = new State(new int[]{27,80,28,81,21,-26,22,-26,23,-26,24,-26,25,-26,26,-26,17,-26,18,-26,37,-26,34,-26},new int[]{-14,49});
    states[80] = new State(-35);
    states[81] = new State(-36);
    states[82] = new State(new int[]{29,83,30,84,27,-34,28,-34,21,-34,22,-34,23,-34,24,-34,25,-34,26,-34,17,-34,18,-34,37,-34,34,-34},new int[]{-15,51});
    states[83] = new State(-39);
    states[84] = new State(-40);
    states[85] = new State(new int[]{19,86,20,87,29,-38,30,-38,27,-38,28,-38,21,-38,22,-38,23,-38,24,-38,25,-38,26,-38,17,-38,18,-38,37,-38,34,-38},new int[]{-16,53});
    states[86] = new State(-43);
    states[87] = new State(-44);
    states[88] = new State(-42);
    states[89] = new State(new int[]{37,90,38,91});
    states[90] = new State(-51);
    states[91] = new State(-52);
    states[92] = new State(new int[]{43,93,41,42,28,57,32,58,31,59,39,60,40,61,45,62,46,63,44,64,33,66,2,89},new int[]{-4,95,-5,69,-6,72,-7,79,-8,82,-9,85,-10,88,-17,55});
    states[93] = new State(new int[]{37,94});
    states[94] = new State(-16);
    states[95] = new State(new int[]{37,96});
    states[96] = new State(-17);
    states[97] = new State(new int[]{41,98});
    states[98] = new State(new int[]{37,99});
    states[99] = new State(-18);
    states[100] = new State(-7);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-19, new int[]{-18,3});
    rules[2] = new Rule(-18, new int[]{4,35,-1,36,38});
    rules[3] = new Rule(-1, new int[]{13,-11,37,-1});
    rules[4] = new Rule(-1, new int[]{11,-11,37,-1});
    rules[5] = new Rule(-1, new int[]{12,-11,37,-1});
    rules[6] = new Rule(-1, new int[]{-2});
    rules[7] = new Rule(-11, new int[]{41});
    rules[8] = new Rule(-2, new int[]{});
    rules[9] = new Rule(-2, new int[]{-3,-2});
    rules[10] = new Rule(-3, new int[]{35,-2,36});
    rules[11] = new Rule(-3, new int[]{5,33,-4,34,-3});
    rules[12] = new Rule(-3, new int[]{5,33,-4,34,-3,6,-3});
    rules[13] = new Rule(-3, new int[]{7,33,-4,34,-3});
    rules[14] = new Rule(-3, new int[]{10,37});
    rules[15] = new Rule(-3, new int[]{-4,37});
    rules[16] = new Rule(-3, new int[]{9,43,37});
    rules[17] = new Rule(-3, new int[]{9,-4,37});
    rules[18] = new Rule(-3, new int[]{8,41,37});
    rules[19] = new Rule(-4, new int[]{41,16,-5});
    rules[20] = new Rule(-4, new int[]{-5});
    rules[21] = new Rule(-5, new int[]{-5,-12,-6});
    rules[22] = new Rule(-5, new int[]{-6});
    rules[23] = new Rule(-12, new int[]{17});
    rules[24] = new Rule(-12, new int[]{18});
    rules[25] = new Rule(-6, new int[]{-6,-13,-7});
    rules[26] = new Rule(-6, new int[]{-7});
    rules[27] = new Rule(-13, new int[]{21});
    rules[28] = new Rule(-13, new int[]{22});
    rules[29] = new Rule(-13, new int[]{23});
    rules[30] = new Rule(-13, new int[]{24});
    rules[31] = new Rule(-13, new int[]{25});
    rules[32] = new Rule(-13, new int[]{26});
    rules[33] = new Rule(-7, new int[]{-7,-14,-8});
    rules[34] = new Rule(-7, new int[]{-8});
    rules[35] = new Rule(-14, new int[]{27});
    rules[36] = new Rule(-14, new int[]{28});
    rules[37] = new Rule(-8, new int[]{-8,-15,-9});
    rules[38] = new Rule(-8, new int[]{-9});
    rules[39] = new Rule(-15, new int[]{29});
    rules[40] = new Rule(-15, new int[]{30});
    rules[41] = new Rule(-9, new int[]{-9,-16,-10});
    rules[42] = new Rule(-9, new int[]{-10});
    rules[43] = new Rule(-16, new int[]{19});
    rules[44] = new Rule(-16, new int[]{20});
    rules[45] = new Rule(-10, new int[]{-17,-10});
    rules[46] = new Rule(-10, new int[]{45});
    rules[47] = new Rule(-10, new int[]{46});
    rules[48] = new Rule(-10, new int[]{44});
    rules[49] = new Rule(-10, new int[]{41});
    rules[50] = new Rule(-10, new int[]{33,-4,34});
    rules[51] = new Rule(-10, new int[]{2,37});
    rules[52] = new Rule(-10, new int[]{2,38});
    rules[53] = new Rule(-17, new int[]{28});
    rules[54] = new Rule(-17, new int[]{32});
    rules[55] = new Rule(-17, new int[]{31});
    rules[56] = new Rule(-17, new int[]{39});
    rules[57] = new Rule(-17, new int[]{40});
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
      case 3: // prestatement -> Bool, declarationstring, Semicolon, prestatement
#line 35 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.bool_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 4: // prestatement -> Int, declarationstring, Semicolon, prestatement
#line 37 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.int_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 5: // prestatement -> Double, declarationstring, Semicolon, prestatement
#line 39 "kompilator.y"
   { CurrentSemanticValue.stat = new Declaration_Statement(TypeOfValue.double_val, ValueStack[ValueStack.Depth-3].value, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 6: // prestatement -> manystatements
#line 41 "kompilator.y"
   { CurrentSemanticValue.stat = ValueStack[ValueStack.Depth-1].stat; }
#line default
        break;
      case 7: // declarationstring -> Identificator
#line 44 "kompilator.y"
                                  { CurrentSemanticValue.value = new Value(ValueStack[ValueStack.Depth-1].val, Compiler.lines); }
#line default
        break;
      case 8: // manystatements -> /* empty */
#line 48 "kompilator.y"
      { CurrentSemanticValue.stat = new Empty_Statement(); }
#line default
        break;
      case 9: // manystatements -> statement, manystatements
#line 50 "kompilator.y"
   { CurrentSemanticValue.stat = new ManyStatements(ValueStack[ValueStack.Depth-2].stat, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 10: // statement -> CurlyBracketLeft, manystatements, CurlyBracketRight
#line 54 "kompilator.y"
   { CurrentSemanticValue.stat = ValueStack[ValueStack.Depth-2].stat; }
#line default
        break;
      case 11: // statement -> If, ParenthesisLeft, expression, ParenthesisRight, statement
#line 56 "kompilator.y"
   { CurrentSemanticValue.stat = new If_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 12: // statement -> If, ParenthesisLeft, expression, ParenthesisRight, statement, Else, 
               //              statement
#line 58 "kompilator.y"
   { CurrentSemanticValue.stat = new If_Statement(ValueStack[ValueStack.Depth-5].expression, ValueStack[ValueStack.Depth-3].stat, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 13: // statement -> While, ParenthesisLeft, expression, ParenthesisRight, statement
#line 60 "kompilator.y"
   { CurrentSemanticValue.stat = new While_Statement(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-1].stat); }
#line default
        break;
      case 14: // statement -> Return, Semicolon
#line 62 "kompilator.y"
   { CurrentSemanticValue.stat = new Return_Statement(); }
#line default
        break;
      case 15: // statement -> expression, Semicolon
#line 64 "kompilator.y"
   { CurrentSemanticValue.stat = new Expression_Statement(ValueStack[ValueStack.Depth-2].expression); }
#line default
        break;
      case 16: // statement -> Write, String, Semicolon
#line 66 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-2].val); }
#line default
        break;
      case 17: // statement -> Write, expression, Semicolon
#line 68 "kompilator.y"
   { CurrentSemanticValue.stat = new Write_Statement(ValueStack[ValueStack.Depth-2].expression); }
#line default
        break;
      case 18: // statement -> Read, Identificator, Semicolon
#line 70 "kompilator.y"
   { CurrentSemanticValue.stat = new Read_Statement(new Value(ValueStack[ValueStack.Depth-2].val, Compiler.lines)); }
#line default
        break;
      case 19: // expression -> Identificator, Assign, A
#line 74 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(new Value(ValueStack[ValueStack.Depth-3].val, Compiler.lines), OperationType.Assign, ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 20: // expression -> A
#line 76 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 21: // A -> A, logical, B
#line 80 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 22: // A -> B
#line 82 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 23: // logical -> ConditionalOr
#line 86 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.ConditionalOr; }
#line default
        break;
      case 24: // logical -> ConditionalAnd
#line 88 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.ConditionalAnd; }
#line default
        break;
      case 25: // B -> B, relation, C
#line 92 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 26: // B -> C
#line 94 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 27: // relation -> Equal
#line 98 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Equal; }
#line default
        break;
      case 28: // relation -> Inequal
#line 100 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Inequal; }
#line default
        break;
      case 29: // relation -> GreaterThan
#line 102 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.GreaterThan; }
#line default
        break;
      case 30: // relation -> GreaterOrEqual
#line 104 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.GreaterOrEqual; }
#line default
        break;
      case 31: // relation -> LessThan
#line 106 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LessThan; }
#line default
        break;
      case 32: // relation -> LessOrEqual
#line 108 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LessOrEqual; }
#line default
        break;
      case 33: // C -> C, oppadd, D
#line 112 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 34: // C -> D
#line 114 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 35: // oppadd -> Plus
#line 118 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Plus; }
#line default
        break;
      case 36: // oppadd -> Minus
#line 120 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Minus; }
#line default
        break;
      case 37: // D -> D, oppmul, E
#line 124 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 38: // D -> E
#line 126 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 39: // oppmul -> Multiplication
#line 130 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Multiplication; }
#line default
        break;
      case 40: // oppmul -> Divide
#line 132 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.Divide; }
#line default
        break;
      case 41: // E -> E, binary, F
#line 136 "kompilator.y"
   { CurrentSemanticValue.expression = new Operand(ValueStack[ValueStack.Depth-3].expression, ValueStack[ValueStack.Depth-2].opptype ,ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 42: // E -> F
#line 138 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-1].expression; }
#line default
        break;
      case 43: // binary -> BooleanLogicalOr
#line 142 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BooleanLogicalOr; }
#line default
        break;
      case 44: // binary -> BooleanLogicalAnd
#line 144 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BooleanLogicalAnd; }
#line default
        break;
      case 45: // F -> unar, F
#line 148 "kompilator.y"
   { CurrentSemanticValue.expression = new UnaryOperand(ValueStack[ValueStack.Depth-2].opptype, ValueStack[ValueStack.Depth-1].expression, Compiler.lines); }
#line default
        break;
      case 46: // F -> IntNumber
#line 150 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].i_val, Compiler.lines); }
#line default
        break;
      case 47: // F -> DoubleNumber
#line 152 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].d_val, Compiler.lines); }
#line default
        break;
      case 48: // F -> BooleanNumber
#line 154 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].b_val, Compiler.lines); }
#line default
        break;
      case 49: // F -> Identificator
#line 156 "kompilator.y"
   { CurrentSemanticValue.expression = new Value(ValueStack[ValueStack.Depth-1].val, Compiler.lines);}
#line default
        break;
      case 50: // F -> ParenthesisLeft, expression, ParenthesisRight
#line 158 "kompilator.y"
   { CurrentSemanticValue.expression = ValueStack[ValueStack.Depth-2].expression; }
#line default
        break;
      case 51: // F -> error, Semicolon
#line 160 "kompilator.y"
      { yyerrok(); Console.WriteLine("error syntax, line: " + Compiler.lines.ToString()); Compiler.errors++;  }
#line default
        break;
      case 52: // F -> error, Eof
#line 162 "kompilator.y"
      { yyerrok(); Console.WriteLine("something missing, line: " + Compiler.lines.ToString()); Compiler.errors++; YYAbort(); }
#line default
        break;
      case 53: // unar -> Minus
#line 166 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.UnaryMinus; }
#line default
        break;
      case 54: // unar -> BitwiseComplement
#line 168 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.BitwiseComplement; }
#line default
        break;
      case 55: // unar -> LogicalNegation
#line 170 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.LogicalNegation; }
#line default
        break;
      case 56: // unar -> IntConversion
#line 172 "kompilator.y"
   { CurrentSemanticValue.opptype = OperationType.IntConversion; }
#line default
        break;
      case 57: // unar -> DoubleConversion
#line 174 "kompilator.y"
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

#line 179 "kompilator.y"

public static Statement program;

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
