using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class TestVisitor : IVisitor
    {
        public void Visit(Program program)
        {
            foreach (INode stmt in program.Statements)
            {
                stmt.Accept(this);
            }

            foreach (KeyValuePair<string, FuncDefinition> funcDef in program.FuncDefinitions)
            {
                funcDef.Value.Accept(this);
            }
        }

        public void Visit(FuncDefinition funcDef)
        {
            Console.Write("func ");
            Console.Write(funcDef.Name);

            Console.Write("(");
            int argsCntr = funcDef.Parameters.Count;
            foreach (VarDeclaration arg in funcDef.Parameters)
            {
                arg.Accept(this);

                if (argsCntr != 1)
                {
                    Console.Write(",");
                    argsCntr--;
                }
            }
            Console.Write(")");

            funcDef.Body.Accept(this);
        }

        public void Visit(AddExpression addExpr)
        {
            int operandCntr = 1;
            addExpr.Operands[0].Accept(this);

            foreach (string oper in addExpr.Operators)
            {
                switch (oper)
                {
                    case "+":
                        Console.Write("+");
                        break;

                    case "-":
                        Console.Write("-");
                        break;
                }

                addExpr.Operands[operandCntr++].Accept(this);
            }
        }

        public void Visit(AssignmentStatement assignStmt)
        {
            Console.Write($"{assignStmt.Variable}=");
            assignStmt.RightSideExpression.Accept(this);
        }

        public void Visit(BlockStatement blockStmt)
        {
            Console.Write("{");
            foreach (INode stmt in blockStmt.Statements)
            {
                stmt.Accept(this);
            }
            Console.Write("}");
        }

        public void Visit(EqualCondition equalCond)
        {
            int operandCntr = 1;
            equalCond.Operands[0].Accept(this);

            foreach (string oper in equalCond.Operators)
            {
                switch (oper)
                {
                    case "==":
                        Console.Write("==");
                        break;

                    case "!=":
                        Console.Write("!=");
                        break;
                }

                equalCond.Operands[operandCntr++].Accept(this);
            }
        }

        public void Visit(ExpressionExprParam exprExprParam)
        {
            Console.Write("(");
            exprExprParam.Expression.Accept(this);
            Console.Write(")");
        }

        public void Visit(FuncCall funcCall)
        {
            Console.Write(funcCall.Name);

            Console.Write("(");
            int argsCntr = funcCall.Arguments.Count;
            foreach (AddExpression arg in funcCall.Arguments)
            {
                arg.Accept(this);

                if (argsCntr != 1)
                {
                    Console.Write(",");
                    argsCntr--;
                }
            }
            Console.Write(")");
        }

        public void Visit(FuncCallExprParam funcCallExprParam)
        {
            Console.Write(funcCallExprParam.Unary ? "-" : "");
            funcCallExprParam.FuncCall.Accept(this);
        }

        public void Visit(IdentifierExprParam identExprParam)
        {
            Console.Write(identExprParam.Unary ? $"-{identExprParam.Value}" : identExprParam.Value);
        }

        public void Visit(IfStatement ifStmt)
        {
            Console.Write("if");

            Console.Write("(");
            ifStmt.Condition.Accept(this);
            Console.Write(")");

            ifStmt.Body.Accept(this);

            if (ifStmt.ElseBody.Statements.Count != 0)
            {
                Console.Write("else");

                ifStmt.ElseBody.Accept(this);
            }
        }

        public void Visit(MethCall methCall)
        {
            Console.Write($"{methCall.TurtleName}.{methCall.MethName}");

            Console.Write("(");
            methCall.Argument?.Accept(this);
            Console.Write(")");
        }

        public void Visit(MultExpression multExpr)
        {
            int operandCntr = 1;
            multExpr.Operands[0].Accept(this);

            foreach (string oper in multExpr.Operators)
            {
                switch (oper)
                {
                    case "*":
                        Console.Write("*");
                        break;

                    case "/":
                        Console.Write("/");
                        break;
                }

                multExpr.Operands[operandCntr++].Accept(this);
            }
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            Console.Write(numValExprParam.Unary ? $"-{numValExprParam.Value}" : numValExprParam.Value.ToString());
        }

        public void Visit(RelationalCondition relCond)
        {
            int operandCntr = 1;
            relCond.Operands[0].Accept(this);

            foreach (string oper in relCond.Operators)
            {
                switch (oper)
                {
                    case "<":
                        Console.Write("<");
                        break;

                    case "<=":
                        Console.Write("<=");
                        break;

                    case ">":
                        Console.Write(">");
                        break;

                    case ">=":
                        Console.Write(">=");
                        break;
                }

                relCond.Operands[operandCntr++].Accept(this);
            }
        }

        public void Visit(RepeatStatement repeatStmt)
        {
            Console.Write("repeat");

            Console.Write("(");
            repeatStmt.NumOfTimes.Accept(this);
            Console.Write(")");

            repeatStmt.Body.Accept(this);
        }

        public void Visit(ReturnStatement retStmt)
        {
            Console.Write("return ");
            retStmt.Expression.Accept(this);
        }

        public void Visit(StrValueExprParam strValExprParam)
        {
            Console.Write($"\"{strValExprParam.Value}\"");
        }

        public void Visit(VarDeclaration varDecl)
        {
            Console.Write($"{varDecl.Type} {varDecl.Name}");
        }
    }
}
