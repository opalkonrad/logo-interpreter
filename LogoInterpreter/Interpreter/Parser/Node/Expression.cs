using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Expression : Node
    {
        public List<Expression> Operands { get; set; } = new List<Expression>();
        public List<Token> Operators { get; set; } = new List<Token>();
        
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
