using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class AddExpression : INode
    {
        public List<MultExpression> Operands { get; set; } = new List<MultExpression>();
        public List<string> Operators { get; set; } = new List<string>();

        public AddExpression()
        {

        }

        public AddExpression(List<MultExpression> operands, List<string> operators)
        {
            Operands = operands;
            Operators = operators;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
