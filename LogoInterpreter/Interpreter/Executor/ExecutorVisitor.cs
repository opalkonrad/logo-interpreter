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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Visit(NumValueExprParam numValExprParam)
        {
            throw new NotImplementedException();
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
            Environment.AddVarDeclaration(varDecl.Name, varDecl.Type);
        }
    }
}
