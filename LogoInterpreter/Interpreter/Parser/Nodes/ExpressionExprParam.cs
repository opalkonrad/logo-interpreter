using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ExpressionExprParam : INode
    {
        public bool Unary { get; set; }
        public AddExpression Expression { get; set; }

        public ExpressionExprParam()
        {

        }

        public ExpressionExprParam(bool unary, AddExpression expr)
        {
            Unary = unary;
            Expression = expr;
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
