using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class RelationalCondition : INode
    {
        public List<AddExpression> Operands { get; set; } = new List<AddExpression>();
        public List<string> Operators { get; set; } = new List<string>();

        public RelationalCondition()
        {

        }

        public RelationalCondition(List<AddExpression> operands, List<string> operators)
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
