using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class BlockStatement : INode
    {
        public List<INode> Statements { get; set; } = new List<INode>();

        public BlockStatement()
        {

        }

        public BlockStatement(List<INode> statements)
        {
            Statements = statements;
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
