using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Executor
{
    class TestVisitor : IVisitor
    {
        public void Visit(Program program)
        {
            throw new NotImplementedException();
        }

        public void Visit(FuncDefinition funcDef)
        {
            throw new NotImplementedException();
        }

        public void Visit(AddExpression addExpr)
        {
            foreach (INode operand in addExpr.Operands)
            {
                operand.Accept(this);
            }
        }

        public void Visit(AssignmentStatement assignStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(BlockStatement blockStmt)
        {
            throw new NotImplementedException();
        }

        public void Visit(EqualCondition equalCond)
        {
            throw new NotImplementedException();
        }

        public void Visit(ExpressionExprParam exprexprparam)
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
            foreach (INode operand in multExpr.Operands)
            {
                operand.Accept(this);
            }
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            numValExprParam.ToString();
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

        public void Visit(VarDeclaration varDecl)
        {
            throw new NotImplementedException();
        }
    }
}
