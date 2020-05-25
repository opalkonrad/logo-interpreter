using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ExpressionExprParam : INode
    {
        public AddExpression Expression { get; set; }

        public ExpressionExprParam()
        {

        }

        public ExpressionExprParam(AddExpression expr)
        {
            Expression = expr;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
