using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public interface IVisitor
    {
        void Visit(Program program);

        void Visit(FuncDefinition funcDef);

        void Visit(AddExpression addExpr);
        void Visit(AssignmentStatement assignStmt);
        void Visit(BlockStatement blockStmt);
        void Visit(EqualCondition equalCond);
        void Visit(ExpressionExprParam exprExprParam);
        void Visit(FuncCall funcCall);
        void Visit(FuncCallExprParam funcCallExprParam);
        void Visit(IdentifierExprParam identExprParam);
        void Visit(IfStatement ifStmt);
        void Visit(MethCall methCall);
        void Visit(MultExpression multExpr);
        void Visit(NumValueExprParam numValExprParam);
        void Visit(RelationalCondition relCond);
        void Visit(RepeatStatement repeatStmt);
        void Visit(ReturnStatement retStmt);
        void Visit(StrValueExprParam strValExprParam);
        void Visit(VarDeclaration varDecl);
    }
}
