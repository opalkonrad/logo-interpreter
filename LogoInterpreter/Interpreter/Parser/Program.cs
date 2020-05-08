using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Program
    {
        public List<Node> Statements { get; set; } = new List<Node>();
        public List<FuncDefinition> FuncDefinitions { get; set; } = new List<FuncDefinition>();
    }
}
