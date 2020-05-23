using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Program : INode
    {
        public List<INode> Statements { get; set; } = new List<INode>();
        public Dictionary<string, FuncDefinition> FuncDefinitions { get; set; } = new Dictionary<string, FuncDefinition>();

        public Program()
        {

        }

        public Program(List<INode> statements, Dictionary<string, FuncDefinition> funcDefs)
        {
            Statements = statements;
            FuncDefinitions = funcDefs;
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
