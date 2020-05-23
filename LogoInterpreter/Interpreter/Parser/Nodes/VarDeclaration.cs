using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class VarDeclaration : INode
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public VarDeclaration()
        {

        }

        public VarDeclaration(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
