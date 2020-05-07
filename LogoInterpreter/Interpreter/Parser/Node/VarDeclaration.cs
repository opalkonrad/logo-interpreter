using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class VarDeclaration : Node
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public VarDeclaration()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
