using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class MethCall : Node
    {
        public string TurtleName { get; set; }
        public string MethName { get; set; }
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
