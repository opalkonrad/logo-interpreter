using LogoInterpreter.Interpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace LogoInterpreter.Tests
{
    [TestClass]
    public class ParserTests
    {
        private readonly StringWriter writer;

        public ParserTests()
        {
            writer = new StringWriter();
            Console.SetOut(writer);
        }

        [TestMethod]
        [DataRow("a=x")]
        [DataRow("b=(x)")]
        [DataRow("c=-x")]
        [DataRow("d=(-x)")]
        [DataRow("e=1+2/3")]
        [DataRow("f=foo(1)")]
        public void ExecutorVisitorVisit_AssignmentStatement_CreateAssignmentStatementAST(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(text, writer.ToString());
        }

        [TestMethod]
        [DataRow("a()")]
        [DataRow("b(x)")]
        [DataRow("c(-x)")]
        [DataRow("d(x,y)")]
        [DataRow("e(x,y,z)")]
        [DataRow("f(10)")]
        [DataRow("g(-10)")]
        [DataRow("h(10,20)")]
        [DataRow("i(10,-20)")]
        [DataRow("j(10,x)")]
        [DataRow("k(x,10)")]
        [DataRow("l(\"lala\")")]
        [DataRow("m(\"lala\",10)")]
        [DataRow("n(\"lala\",10,x)")]
        public void ExecutorVisitorVisit_FuncCall_CreateFuncCallAST(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(text, writer.ToString());
        }

        [TestMethod]
        [DataRow("func a(){}", "func a(){}")]
        [DataRow("func b(num x){}", "func b(NumToken x){}")]
        [DataRow("func c(num x){num y}", "func c(NumToken x){NumToken y}")]
        public void ExecutorVisitorVisit_FuncDefinition_CreateFuncDefinitionAST(string text, string expected)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(expected, writer.ToString());
        }

        [TestMethod]
        [DataRow("if(1){}", "if(1){}")]
        [DataRow("if(-1){}", "if(-1){}")]
        [DataRow("if(1<0){}", "if(1<0){}")]
        [DataRow("if(1<=0){}", "if(1<=0){}")]
        [DataRow("if(1>0){}", "if(1>0){}")]
        [DataRow("if(1>0){}", "if(1>0){}")]
        [DataRow("if(1>=0){}", "if(1>=0){}")]
        [DataRow("if(1){}else{}", "if(1){}")]
        [DataRow("if(-1){}else{}", "if(-1){}")]
        [DataRow("if(1){num x}else{}", "if(1){NumToken x}")]
        [DataRow("if(1){}else{num x}", "if(1){}else{NumToken x}")]
        public void ExecutorVisitorVisit_IfStatement_CreateIfStatementAST(string text, string expected)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(expected, writer.ToString());
        }

        [TestMethod]
        [DataRow("a.Forward(100)")]
        [DataRow("b.Right(90)")]
        [DataRow("c.PenUp()")]
        public void ExecutorVisitorVisit_MethCall_CreateMethCallAST(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(text, writer.ToString());
        }

        [TestMethod]
        [DataRow("repeat(1){}")]
        [DataRow("repeat(-1){}")]
        public void ExecutorVisitorVisit_RepeatStatement_CreateRepeatStatementAST(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(text, writer.ToString());
        }

        [TestMethod]
        [DataRow("return 1")]
        [DataRow("return -1")]
        [DataRow("return x")]
        [DataRow("return -x")]
        [DataRow("return \"lala\"")]
        [DataRow("return 5-1")]
        public void ExecutorVisitorVisit_ReturnStatement_CreateReturnStatementAST(string text)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(text, writer.ToString());
        }

        [TestMethod]
        [DataRow("str a", "StrToken a")]
        [DataRow("num b", "NumToken b")]
        [DataRow("Turtle c", "TurtleToken c")]
        public void ExecutorVisitorVisit_VarDeclaration_CreateVarDeclarationAST(string text, string expected)
        {
            var lexer = new Lexer(new StringSource(text));
            var parser = new Parser(lexer);
            var program = parser.Parse();
            var testVisitor = new TestVisitor();
            testVisitor.Visit(program);
            Assert.AreEqual(expected, writer.ToString());
        }
    }
}
