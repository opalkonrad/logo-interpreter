using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogoInterpreter.Interpreter
{
    public class ExecutorVisitor : IVisitor
    {
        public Environment Environment { get; private set; }
        public Program Program { get; private set; }
        private readonly Canvas canvas;
        private bool inFunction;
        private bool wasReturn;

        public ExecutorVisitor(Canvas canvas)
        {
            Environment = new Environment();
            this.canvas = canvas;
            inFunction = false;
            wasReturn = false;
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

            // Return statements now can be applied
            inFunction = true;

            // Add arguments to new scope
            for (int currParam = funcDef.Parameters.Count - 1; currParam >= 0; currParam--)
            {
                dynamic lastArg = Environment.PopFromTheStack();

                if (lastArg is double && funcDef.Parameters[currParam].Type == "NumToken")
                {
                    Environment.AddItem(new NumItem(funcDef.Parameters[currParam].Name, lastArg));
                }
                else if (lastArg is string && funcDef.Parameters[currParam].Type == "StrToken")
                {
                    Environment.AddItem(new StrItem(funcDef.Parameters[currParam].Name, lastArg));
                }
                else if (lastArg is TurtleItem && funcDef.Parameters[currParam].Type == "TurtleToken")
                {
                    Environment.AddReferenceToTurtle(funcDef.Parameters[currParam].Name, Environment.GetItem(lastArg.Name));
                }
            }

            funcDef.Body.Accept(this);

            inFunction = false;
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
                    _ => throw new ExecutorException($"Unknown additive operator {oper}")
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(AssignmentStatement assignStmt)
        {
            // Count right side of expression and place result on the stack
            assignStmt.RightSideExpression.Accept(this);

            // Find variable in scope and try to assign it a value
            Item currItem = Environment.GetItem(assignStmt.Variable);
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

                // Interpreter noticed return statement and leaves function
                if (wasReturn && inFunction)
                {
                    wasReturn = false;
                    return;
                }
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
            // Check parameters count
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
                throw new ExecutorException($"Wrong number of arguments in function call called {funcCall.Name}");
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
            Item ident = Environment.GetItem(identExprParam.Value);

            switch (ident)
            {
                case StrItem _:
                    Environment.PushToTheStack(identExprParam.Unary == false ? (ident as StrItem).Value
                        : throw new ExecutorException("Cannot apply unary operator on str"));
                    break;

                case NumItem _:
                    Environment.PushToTheStack(identExprParam.Unary == true ? (ident as NumItem).Value * (-1)
                        : (ident as NumItem).Value);
                    break;

                case TurtleItem _:
                    Environment.PushToTheStack(identExprParam.Unary == false ? ident
                        : throw new ExecutorException("Cannot apply unary operator on Turtle"));
                    break;

                default:
                    throw new ExecutorException($"Expecting identifier of one of types: str, num or Turtle, got {ident}");
            }
        }

        public void Visit(IfStatement ifStmt)
        {
            ifStmt.Condition.Accept(this);

            dynamic cond = Environment.PopFromTheStack();

            Environment.NewScope();

            if ((cond is double && cond > 0) || (cond is bool && cond))
            {
                ifStmt.Body.Accept(this);
            }
            else
            {
                ifStmt.ElseBody.Accept(this);
            }

            Environment.DeleteScope();
        }

        public void Visit(MethCall methCall)
        {
            if (Environment.GetItem(methCall.TurtleName) is TurtleItem turle)
            {
                // Check for arguments
                if (methCall.Argument != null)
                {
                    methCall.Argument.Accept(this);

                    dynamic arg = Environment.PopFromTheStack();

                    // Color or identifier
                    if (arg is string)
                    {
                        if (!(arg == "Black" || arg == "Red" || arg == "Green" || arg == "Blue"))
                        {
                            arg = Environment.GetItem(arg).Value;
                        }
                    }

                    // Built-in methods with arguments
                    switch (methCall.MethName)
                    {
                        case "Forward":
                            turle.Forward(arg);
                            break;

                        case "Backward":
                            turle.Backward(arg);
                            break;

                        case "Right":
                            turle.Right(arg);
                            break;

                        case "Left":
                            turle.Left(arg);
                            break;

                        case "LineColor":
                            turle.LineColor(arg);
                            break;

                        case "LineThickness":
                            turle.LineThickness(arg);
                            break;

                        default:
                            throw new ExecutorException($"No built-in method called {methCall.MethName} found");
                    }
                }
                else
                {
                    // Built-in methods with no arguments
                    switch (methCall.MethName)
                    {
                        case "PenUp":
                            turle.PenUp();
                            break;

                        case "PenDown":
                            turle.PenDown();
                            break;

                        default:
                            throw new ExecutorException($"No built-in method called {methCall.MethName} found");
                    }
                }
            }
            else
            {
                throw new ExecutorException($"Method with the name: {methCall} can only be applied on Turtle object");
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
                    _ => throw new ExecutorException($"Unknown multiplicative operator {oper}")
                };

                Environment.PushToTheStack(result);
            }
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            Environment.PushToTheStack(numValExprParam.Unary == true ? numValExprParam.Value * (-1) : numValExprParam.Value);
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
                    _ => throw new ExecutorException($"Unknown relational operator {oper}")
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
                    Environment.NewScope();

                    repeatStmt.Body.Accept(this);

                    Environment.DeleteScope();
                }
            }
            else
            {
                throw new ExecutorException($"Number of times: {numOfTimes} in repeat statement should be a digit");
            }
        }

        public void Visit(ReturnStatement retStmt)
        {
            // Return Statements can only be applied in function
            if (inFunction && !wasReturn)
            {
                retStmt.Expression.Accept(this);
                wasReturn = true;
            }
            else
            {
                throw new ExecutorException("Cannot return value while not in function");
            }
        }

        public void Visit(StrValueExprParam strValExprParam)
        {
            Environment.PushToTheStack(strValExprParam.Value);
        }

        public void Visit(VarDeclaration varDecl)
        {
            if (varDecl.Type == "TurtleToken")
            {
                Environment.AddItem(new TurtleItem(varDecl.Name, canvas));
            }
            else
            {
                Environment.AddVarDeclaration(varDecl.Name, varDecl.Type);
            }
        }
    }
}
