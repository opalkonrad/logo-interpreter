using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class AssignmentStatement : Node
    {
        public string Name { get; set; }
        public Expression RightSideExpression { get; set; }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
