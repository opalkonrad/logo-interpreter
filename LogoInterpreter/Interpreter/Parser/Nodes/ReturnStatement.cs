using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ReturnStatement : INode
    {
        public AddExpression Expression { get; set; }

        public ReturnStatement()
        {

        }

        public ReturnStatement(AddExpression expr)
        {
            Expression = expr;
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
