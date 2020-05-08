using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class MethCall : Node
    {
        public string Turtle { get; set; }
        public string Name { get; set; }
        public Expression Argument { get; set; }

        public MethCall()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
