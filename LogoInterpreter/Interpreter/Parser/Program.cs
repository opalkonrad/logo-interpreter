using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Program
    {
        public List<Node> Statements { get; set; } = new List<Node>();
        public List<FuncDefinition> FuncDefinitions { get; set; } = new List<FuncDefinition>();

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("Function Definitions\n");
            foreach (FuncDefinition fd in FuncDefinitions)
            {
                result.Append(fd.ToString());

                result.Append("\nFunction Definitions Statements:\n");

                foreach (Node stmt in fd.Body.Statements)
                {
                    result.Append(stmt.ToString());
                }
            }

            result.Append("\nStatements:\n");
            foreach (Node stmt in Statements)
            {
                result.Append(stmt.ToString());
            }

            return result.ToString();
        }
    }
}
