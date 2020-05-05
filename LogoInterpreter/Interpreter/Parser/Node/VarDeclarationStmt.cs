using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class VarDeclarationStmt : Statement
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public VarDeclarationStmt()
        {

        }

        public VarDeclarationStmt(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public override void Execute(Environment environment)
        {
            environment.DeclareNewVar(Name, Type);
        }
    }
}
