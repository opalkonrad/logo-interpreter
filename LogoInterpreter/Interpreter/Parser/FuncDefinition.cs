using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class FuncDefinition : Node
    {
        public string Name { get; set; }
        public List<VarDeclaration> Parameters { get; set; }
        public BlockStatement Body { get; set; }

        public FuncDefinition()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
