using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class BlockStatement : Statement
    {
        public List<Statement> Statements { get; set; }

        public BlockStatement()
        {

        }

        public BlockStatement(List<Statement> statements)
        {
            Statements = statements;
        }

        public override void Execute(Environment environment)
        {
            environment.CreateLocalScope();

            foreach (Statement stmt in Statements)
            {
                stmt.Execute(environment);
            }

            environment.DestroyLocalScope();
        }
    }
}
