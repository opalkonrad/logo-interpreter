using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class FuncDefinition
    {
        public string Name { get; set; }
        public List<VarDeclarationStmt> Parameters { get; set; }
        public BlockStatement Body { get; set; }
        //public string ReturnType { get; }

        public FuncDefinition()
        {

        }

        public FuncDefinition(string name, List<VarDeclarationStmt> parameters, BlockStatement body)
        {
            Name = name;
            Parameters = parameters;
            Body = body;
            //environment.DeclareNewVar(name, "Func");
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Name:\n  {Name}\n");
            stringBuilder.Append("Parameters:\n");
            foreach (VarDeclarationStmt parameter in Parameters)
            {
                stringBuilder.Append($"  {parameter.Type}, {parameter.Name}\n");
            }

            return stringBuilder.ToString();
        }
    }
}
