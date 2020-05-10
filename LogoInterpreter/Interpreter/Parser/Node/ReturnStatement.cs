using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ReturnStatement : Node
    {
        public Expression Expression { get; set; }

        public override void Accept(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
