using LogoInterpreter.Interpreter.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogoInterpreter.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        [DataRow(" ")]
        [DataRow("\n")]
        [DataRow("\r\n")]
        public void NextToken_TextWithNoInformation_SetTokenToEndOfTextToken(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(EndOfTextToken));
        }

        [TestMethod]
        [ExpectedException(typeof(LexerException))]
        [DataRow("#")]
        [DataRow("~")]
        [DataRow("?")]
        public void NextToken_NotExistingToken_ThrowLexerException(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
        }

        [TestMethod]
        public void NextToken_AndChar_SetTokenToAndToken()
        {
            var lexer = new Lexer(new StringSource("&"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(AndToken));
        }

        [TestMethod]
        public void NextToken_AssignmentChar_SetTokenToAssignmentToken()
        {
            var lexer = new Lexer(new StringSource("="));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(AssignmentToken));
        }


        [TestMethod]
        public void NextToken_AsteriskChar_SetTokenToAssignmentToken()
        {
            var lexer = new Lexer(new StringSource("*"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(AsteriskToken));
        }

        [TestMethod]
        public void NextToken_BlueKeyword_SetTokenToBlueToken()
        {
            var lexer = new Lexer(new StringSource("Blue"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(BlueToken));
        }

        [TestMethod]
        public void NextToken_CommaChar_SetTokenToCommaToken()
        {
            var lexer = new Lexer(new StringSource(","));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(CommaToken));
        }

        [TestMethod]
        public void NextToken_DotChar_SetTokenToDotToken()
        {
            var lexer = new Lexer(new StringSource("."));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(DotToken));
        }

        [TestMethod]
        public void NextToken_ElseKeyword_SetTokenToElseToken()
        {
            var lexer = new Lexer(new StringSource("else"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(ElseToken));
        }

        [TestMethod]
        public void NextToken_EndOfText_SetTokenToEndOfTextToken()
        {
            var lexer = new Lexer(new StringSource(""));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(EndOfTextToken));
        }

        [TestMethod]
        public void NextToken_EqualString_SetTokenToEqualToken()
        {
            var lexer = new Lexer(new StringSource("=="));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(EqualToken));
        }

        [TestMethod]
        public void NextToken_FuncKeyword_SetTokenToFuncToken()
        {
            var lexer = new Lexer(new StringSource("func"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(FuncToken));
        }

        [TestMethod]
        public void NextToken_GreaterEqualThanString_SetTokenToGreaterEqualThanToken()
        {
            var lexer = new Lexer(new StringSource(">="));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(GreaterEqualThanToken));
        }

        [TestMethod]
        public void NextToken_GreaterThanString_SetTokenToGreaterThanToken()
        {
            var lexer = new Lexer(new StringSource(">"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(GreaterThanToken));
        }

        [TestMethod]
        public void NextToken_GreenKeyword_SetTokenToGreenToken()
        {
            var lexer = new Lexer(new StringSource("Green"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(GreenToken));
        }

        [TestMethod]
        [DataRow("myFavTurtle")]
        [DataRow("MyFavTurtle")]
        [DataRow("turtle0")]
        [DataRow("green")]
        public void NextToken_IdentifierString_SetTokenToIdentifierToken(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(IdentifierToken));
        }

        [TestMethod]
        public void NextToken_IfKeyword_SetTokenToIfToken()
        {
            var lexer = new Lexer(new StringSource("if"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(IfToken));
        }

        [TestMethod]
        public void NextToken_InputKeyword_SetTokenToInputToken()
        {
            var lexer = new Lexer(new StringSource("input"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(InputToken));
        }

        [TestMethod]
        public void NextToken_LessEqualThanString_SetTokenToLessEqualThanToken()
        {
            var lexer = new Lexer(new StringSource("<="));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(LessEqualThanToken));
        }

        [TestMethod]
        public void NextToken_LessThanString_SetTokenToLessThanToken()
        {
            var lexer = new Lexer(new StringSource("<"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(LessThanToken));
        }

        [TestMethod]
        public void NextToken_LeftRoundBracketChar_SetTokenToLeftRoundBracketToken()
        {
            var lexer = new Lexer(new StringSource("("));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(LRoundBracketToken));
        }

        [TestMethod]
        public void NextToken_LeftSquareBracketChar_SetTokenToLeftSquareBracketToken()
        {
            var lexer = new Lexer(new StringSource("{"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(LSquareBracketToken));
        }

        [TestMethod]
        public void NextToken_MinusChar_SetTokenToMinusToken()
        {
            var lexer = new Lexer(new StringSource("-"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(MinusToken));
        }

        [TestMethod]
        public void NextToken_NewKeyword_SetTokenToNewToken()
        {
            var lexer = new Lexer(new StringSource("new"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(NewToken));
        }

        [TestMethod]
        public void NextToken_NumKeyword_SetTokenToNumToken()
        {
            var lexer = new Lexer(new StringSource("num"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(NumToken));
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("0.0")]
        [DataRow("1.0")]
        [DataRow("1.001")]
        [DataRow("1.100")]
        public void NextToken_NumValueString_SetTokenToNumValueToken(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(NumValueToken));
        }

        [TestMethod]
        [DataRow("0", 0)]
        [DataRow("0.0", 0)]
        [DataRow("1.0", 1)]
        [DataRow("1.001", 1.001)]
        [DataRow("1.100", 1.1)]
        public void NextToken_NumValueString_SetProperValueOfToken(string text, double expected)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.AreEqual((lexer.Token as NumValueToken).Value, expected);
        }

        [TestMethod]
        [DataRow("0.")]
        [DataRow("0a")]
        [DataRow("0.0a")]
        [DataRow("0.00a")]
        [DataRow("00.1")]
        [DataRow("00.a")]
        [DataRow("00.")]
        [DataRow("01.")]
        [DataRow("01.0")]
        [DataRow("0a.")]
        [DataRow("0a.0")]
        [DataRow("0.a")]
        [DataRow("01.a")]
        [DataRow("0.1a")]
        public void NextToken_WrongNumValueString_ThrowLexerException(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (LexerException)
            {

            }
        }

        [TestMethod]
        public void NextToken_OrChar_SetTokenToOrToken()
        {
            var lexer = new Lexer(new StringSource("|"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(OrToken));
        }

        [TestMethod]
        public void NextToken_PlusChar_SetTokenToPlusToken()
        {
            var lexer = new Lexer(new StringSource("+"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(PlusToken));
        }

        [TestMethod]
        public void NextToken_PrintKeyword_SetTokenToPrintToken()
        {
            var lexer = new Lexer(new StringSource("print"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(PrintToken));
        }

        [TestMethod]
        public void NextToken_RedKeyword_SetTokenToRedToken()
        {
            var lexer = new Lexer(new StringSource("Red"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(RedToken));
        }

        [TestMethod]
        public void NextToken_RepeatKeyword_SetTokenToRepeatToken()
        {
            var lexer = new Lexer(new StringSource("repeat"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(RepeatToken));
        }

        [TestMethod]
        public void NextToken_ReturnKeyword_SetTokenToReturnToken()
        {
            var lexer = new Lexer(new StringSource("return"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(ReturnToken));
        }

        [TestMethod]
        public void NextToken_RightRoundBracketChar_SetTokenToRightRoundBracketToken()
        {
            var lexer = new Lexer(new StringSource(")"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(RRoundBracketToken));
        }

        [TestMethod]
        public void NextToken_RightSquareBracketChar_SetTokenToRightSquareBracketToken()
        {
            var lexer = new Lexer(new StringSource("}"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(RSquareBracketToken));
        }

        [TestMethod]
        public void NextToken_SemicolonChar_SetTokenToSemicolonToken()
        {
            var lexer = new Lexer(new StringSource(";"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(SemicolonToken));
        }

        [TestMethod]
        public void NextToken_SlashChar_SetTokenToSlashToken()
        {
            var lexer = new Lexer(new StringSource("/"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(SlashToken));
        }

        [TestMethod]
        public void NextToken_StrKeyword_SetTokenToStrToken()
        {
            var lexer = new Lexer(new StringSource("str"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(StrToken));
        }

        [TestMethod]
        [DataRow("\"0\"")]
        [DataRow("\"a\"")]
        [DataRow("\"a b\"")]
        [DataRow("\"0 b\"")]
        [DataRow("\"a 0\"")]
        [DataRow("\"#9 0/.=) if\"")]
        public void NextToken_StrValueString_SetTokenToStrValueToken(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(StrValueToken));
        }

        [TestMethod]
        [DataRow("\"\"", "")]
        [DataRow("\"0\"", "0")]
        [DataRow("\"a\"", "a")]
        [DataRow("\"a b\"", "a b")]
        [DataRow("\"0 b\"", "0 b")]
        [DataRow("\"a 0\"", "a 0")]
        [DataRow("\"#9 0/.=) if\"", "#9 0/.=) if")]
        public void NextToken_StrValueString_SetProperValueOfToken(string text, string expected)
        {
            var lexer = new Lexer(new StringSource(text));
            lexer.NextToken();
            Assert.AreEqual((lexer.Token as StrValueToken).Value, expected);
        }

        [TestMethod]
        public void NextToken_NoClosingBracketInStrValueString_ThrowLexerException()
        {
            var lexer = new Lexer(new StringSource("\""));
            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch
            {

            }
        }

        [TestMethod]
        public void NextToken_TurtleKeyword_SetTokenToTurtleToken()
        {
            var lexer = new Lexer(new StringSource("Turtle"));
            lexer.NextToken();
            Assert.IsInstanceOfType(lexer.Token, typeof(TurtleToken));
        }
    }
}
