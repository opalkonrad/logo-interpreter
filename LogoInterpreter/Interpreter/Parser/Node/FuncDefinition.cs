using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class FuncDefinition : INode
    {
        public string Name { get; set; }
        public List<VarDeclaration> Parameters { get; set; }
        public BlockStatement Body { get; set; }

        public FuncDefinition()
        {

        }

        public FuncDefinition(string name, List<VarDeclaration> parameters, BlockStatement body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
