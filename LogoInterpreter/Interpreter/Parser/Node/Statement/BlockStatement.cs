using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class BlockStatement : Statement
    {
        public List<Statement> statements;

        public override void Execute(Environment environment)
        {
            environment.CreateLocalScope();

            foreach (Statement stmt in statements)
            {
                stmt.Execute(environment);
            }

            environment.DestroyLocalScope();
        }
    }
}
