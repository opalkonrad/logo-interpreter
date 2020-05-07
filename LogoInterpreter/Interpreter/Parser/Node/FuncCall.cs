using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class FuncCall : Node
    {
        public string Name { get; set; }
        public List<string> Arguments { get; set; } = new List<string>();

        public FuncCall()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
