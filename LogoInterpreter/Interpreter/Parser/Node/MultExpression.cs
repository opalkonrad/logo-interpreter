using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class MultExpression : INode
    {
        public List<INode> Operands { get; set; } = new List<INode>();
        public List<string> Operators { get; set; } = new List<string>();

        public MultExpression()
        {

        }

        public MultExpression(List<INode> operands, List<string> operators)
        {
            Operands = operands;
            Operators = operators;
        }
        
        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
