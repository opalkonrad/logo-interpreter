using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class RepeatStatement : Node
    {
        

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
