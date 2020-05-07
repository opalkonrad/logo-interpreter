using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Parameter : Node
    {
        public string Type { get; }
        public string Name { get; }

        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public Parameter(VarDeclaration statement)
        {
            Type = statement.Type;

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
