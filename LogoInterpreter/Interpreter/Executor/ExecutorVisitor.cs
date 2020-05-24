using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ExecutorVisitor : IVisitor
    {
        public Environment Environment { get; private set; }

        public ExecutorVisitor()
        {
            Environment = new Environment();
        }

        public void Visit(Program program)
        {
            Environment.NewScope();

            // Execute statements
            foreach (INode stmt in program.Statements)
            {
                stmt.Accept(this);
            }
        }

        public void Visit(FuncDefinition funcDef)
        {
            throw new NotImplementedException();
        }

        public void Visit(AddExpression addExpr)
        {
            // No operators -> count lower layers of expression
            if (addExpr.Operators.Count == 0)
            {
                addExpr.Operands[0].Accept(this);
            }

            int operandCntr = 0;

            foreach (string oper in addExpr.Operators)
            {
                addExpr.Operands[operandCntr++].Accept(this);
                addExpr.Operands[operandCntr++].Accept(this);

                switch (oper)
                {
                    case "+":

                        break;

                    case "-":

                        break;
                }
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
            /*else if (item is TurtleToken)*/
        }

        public void Visit(BlockStatement blockStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(EqualCondition equalCond)
        {
            throw new NotImplementedException();
        }

        public void Visit(ExpressionExprParam exprExprParam)
        {
            throw new NotImplementedException();
        }

        public void Visit(FuncCall funcCall)
        {
            throw new NotImplementedException();
        }

        public void Visit(FuncCallExprParam funcCallExprParam)
        {
            throw new NotImplementedException();
        }

        public void Visit(IdentifierExprParam identExprParam)
        {
            throw new NotImplementedException();
        }

        public void Visit(IfStatement ifStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(MethCall methCall)
        {
            throw new NotImplementedException();
        }

        public void Visit(MultExpression multExpr)
        {
            // No operators -> count lower layers of expression
            if (multExpr.Operators.Count == 0)
            {
                multExpr.Operands[0].Accept(this);
            }
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            Environment.PutOnTheStack(numValExprParam.Unary == true ?
                numValExprParam.Value * (-1) : numValExprParam.Value);
        }

        public void Visit(RelationalCondition relCond)
        {
            throw new NotImplementedException();
        }

        public void Visit(RepeatStatement repeatStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(ReturnStatement retStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(StrValueExprParam strValExprParam)
        {
            Environment.PutOnTheStack(strValExprParam.Value);
        }

        public void Visit(VarDeclaration varDecl)
        {
            Environment.AddVarDeclaration(varDecl.Name, varDecl.Type);
        }
    }
}
