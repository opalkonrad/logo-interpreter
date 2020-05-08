using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class IfStatement : Node
    {
        public Expression Condition { get; set; }
        public BlockStatement Body { get; set; }
        public BlockStatement ElseBody { get; set; }
        
        public IfStatement()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
