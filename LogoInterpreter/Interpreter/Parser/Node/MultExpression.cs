using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class MultExpression : Node
    {
        public bool Unary { get; set; } = false;
        public Expression Expression { get; set; }
        public double Value { get; set; }
        public string Identifier { get; set; }
        public FuncCall FuncCall { get; set; }
        // TODO color_val

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
