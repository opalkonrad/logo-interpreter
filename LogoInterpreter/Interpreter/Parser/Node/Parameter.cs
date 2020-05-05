using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Parameter
    {
        public string Type { get; }
        public string Name { get; }

        public Parameter(string type, string name)
        {
            Type = type;
            Name = name;
        }
        public Parameter(VarDeclarationStmt statement)
        {
            Type = statement.Type;

        }
    }
}
