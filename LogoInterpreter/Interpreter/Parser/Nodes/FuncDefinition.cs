using System.Collections.Generic;

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

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
