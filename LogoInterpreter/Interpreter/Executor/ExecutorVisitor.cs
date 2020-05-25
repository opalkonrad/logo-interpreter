using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Controls;

namespace LogoInterpreter.Interpreter
{
    public class ExecutorVisitor : IVisitor
    {
        public Environment Environment { get; private set; }
        public Program Program { get; private set; }
        private Canvas canvas;

        public ExecutorVisitor(Canvas canvas)
        {
            Environment = new Environment();
            this.canvas = canvas;
        }

        public void Visit(Program program)
        {
            Program = program;
            Environment.NewScope();

            // Execute statements
            foreach (INode stmt in program.Statements)
            {
                stmt.Accept(this);
            }
        }

        public void Visit(FuncDefinition funcDef)
        {
            Environment.NewScope();

            // Add arguments to new scope
            foreach (VarDeclaration param in funcDef.Parameters)
            {
                dynamic lastArg = Environment.PopFromTheStack();

                if (lastArg is double && param.Type == "NumToken")
                {
                    Environment.AddVarDeclaration(param.Name, "NumToken");
                }
                else if (lastArg is string && param.Type == "StrToken")
                {
                    Environment.AddVarDeclaration(param.Name, "StrToken");
                }
                // Turtles are reference type objects
                /*else if (lastArg is TurtleItem && param.Type == "TurtleToken")
                {
                    //Environment.AddVarDeclaration(param.Name, "TurtleToken");
                }*/
            }

            funcDef.Body.Accept(this);

            Environment.DeleteScope();
        }

        public void Visit(AddExpression addExpr)
        {
            int operandCntr = 1;
            addExpr.Operands[0].Accept(this);

            foreach (string oper in addExpr.Operators)
            {
                addExpr.Operands[operandCntr++].Accept(this);

                dynamic rightOper = Environment.PopFromTheStack();
                dynamic leftOper = Environment.PopFromTheStack();

                if (rightOper.GetType() != leftOper.GetType()) // TODO turtle type
                {
                    throw new ExecutorException("Cannot perform an arithmetic operation on operands of different types");
                }

                dynamic result = oper switch
                {
                    "+" => leftOper + rightOper,
                    "-" => leftOper - rightOper,
                    _ => null,
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(AssignmentStatement assignStmt)
        {
            // Count right side of expression and place result on the stack
            assignStmt.RightSideExpression.Accept(this);

            // Find variable in scope and try to assign it a value
            Item currItem = Environment.GetVarValue(assignStmt.Variable);
            dynamic varFromStack = Environment.PopFromTheStack();

            if (currItem is StrItem && varFromStack is string)
            {
                (currItem as StrItem).Value = varFromStack;
            }
            else if (currItem is NumItem && varFromStack is double)
            {
                (currItem as NumItem).Value = varFromStack;
            }
            else
            {
                throw new ExecutorException("Cannot assign "); // TODO
            }
        }

        public void Visit(BlockStatement blockStmt)
        {
            foreach (INode statement in blockStmt.Statements)
            {
                statement.Accept(this);
            }
        }

        public void Visit(EqualCondition equalCond)
        {
            int operandCntr = 1;
            equalCond.Operands[0].Accept(this);

            foreach (string oper in equalCond.Operators)
            {
                equalCond.Operands[operandCntr++].Accept(this);

                dynamic rightOper = Environment.PopFromTheStack();
                dynamic leftOper = Environment.PopFromTheStack();

                if (rightOper.GetType() != leftOper.GetType())
                {
                    throw new ExecutorException("Cannot perform relational condition on operands different than double");
                }

                dynamic result = oper switch
                {
                    "==" => leftOper == rightOper ? true : false,
                    "!=" => leftOper == rightOper ? false : true,
                    _ => null,
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(ExpressionExprParam exprExprParam)
        {
            exprExprParam.Expression.Accept(this);
        }

        public void Visit(FuncCall funcCall)
        {
            if (funcCall.Arguments.Count == Program.FuncDefinitions[funcCall.Name].Parameters.Count)
            {
                foreach (INode arg in funcCall.Arguments)
                {
                    arg.Accept(this);
                }

                Program.FuncDefinitions[funcCall.Name].Accept(this);
            }
            else
            {
                throw new EvaluateException("Wrong number of arguments in function call");
            }
        }

        public void Visit(FuncCallExprParam funcCallExprParam)
        {
            funcCallExprParam.FuncCall.Accept(this);

            if (funcCallExprParam.Unary)
            {
                Environment.PushToTheStack(Environment.PopFromTheStack() * (-1));
            }
        }

        public void Visit(IdentifierExprParam identExprParam)
        {
            Item ident = Environment.GetVarValue(identExprParam.Value);

            switch (ident)
            {
                case StrItem _:
                    Environment.PushToTheStack((ident as StrItem).Value);
                    break;

                case NumItem _:
                    Environment.PushToTheStack((ident as NumItem).Value);
                    break;

                case TurtleItem _:
                    Environment.PushToTheStack(ident);
                    break;

                default:
                    throw new ExecutorException("Expecting identifier of one of types: str or num");
            }
        }

        public void Visit(IfStatement ifStmt)
        {
            ifStmt.Condition.Accept(this);

            dynamic cond = Environment.PopFromTheStack();

            if (cond)
            {
                ifStmt.Body.Accept(this);
            }
            else
            {
                ifStmt.ElseBody.Accept(this);
            }
        }

        public void Visit(MethCall methCall)
        {
            TurtleItem turtle = (TurtleItem)Environment.GetVarValue(methCall.TurtleName);

            methCall.Argument.Accept(this);

            switch (methCall.MethName)
            {
                case "Fd":
                    turtle.Fd(Environment.PopFromTheStack());
                    break;
            }
        }

        public void Visit(MultExpression multExpr)
        {
            int operandCntr = 1;
            multExpr.Operands[0].Accept(this);

            foreach (string oper in multExpr.Operators)
            {
                multExpr.Operands[operandCntr++].Accept(this);

                dynamic rightOper = Environment.PopFromTheStack();
                dynamic leftOper = Environment.PopFromTheStack();

                if (rightOper.GetType() != leftOper.GetType())
                {
                    throw new ExecutorException("Cannot perform an arithmetic operation on operands of different types");
                }

                dynamic result = oper switch
                {
                    "*" => leftOper * rightOper,
                    "/" => leftOper / rightOper,
                    _ => null,
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            Environment.PushToTheStack(numValExprParam.Unary == true ?
                numValExprParam.Value * (-1) : numValExprParam.Value);
        }

        public void Visit(RelationalCondition relCond)
        {
            int operandCntr = 1;
            relCond.Operands[0].Accept(this);

            foreach (string oper in relCond.Operators)
            {
                relCond.Operands[operandCntr++].Accept(this);

                dynamic rightOper = Environment.PopFromTheStack();
                dynamic leftOper = Environment.PopFromTheStack();

                if (rightOper.GetType() != leftOper.GetType())
                {
                    throw new ExecutorException("Cannot perform relational condition on operands different than double");
                }

                dynamic result = oper switch
                {
                    "<" => leftOper < rightOper ? true : false,
                    "<=" => leftOper <= rightOper ? true : false,
                    ">" => leftOper > rightOper ? true : false,
                    ">=" => leftOper >= rightOper ? true : false,
                    _ => null,
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(RepeatStatement repeatStmt)
        {
            repeatStmt.NumOfTimes.Accept(this);

            dynamic numOfTimes = Environment.PopFromTheStack();

            if (numOfTimes is double)
            {
                for (int loopCntr = 0; loopCntr < numOfTimes; loopCntr++)
                {
                    repeatStmt.Body.Accept(this);
                }
            }
            else
            {
                throw new ExecutorException("Number of times in repeat statement is not a digit");
            }
        }

        public void Visit(ReturnStatement retStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(StrValueExprParam strValExprParam)
        {
            Environment.PushToTheStack(strValExprParam.Value);
        }

        public void Visit(VarDeclaration varDecl)
        {
            if (varDecl.Type == "TurtleToken")
            {
                Environment.AddVarDeclaration(new TurtleItem(varDecl.Name, canvas));
            }
            else
            {
                Environment.AddVarDeclaration(varDecl.Name, varDecl.Type);
            }
        }
    }
}
