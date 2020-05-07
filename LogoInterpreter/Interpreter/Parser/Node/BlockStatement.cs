﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class BlockStatement : Node
    {
        public List<Node> Statements { get; set; } = new List<Node>();

        public BlockStatement()
        {

        }

        public BlockStatement(List<Node> statements)
        {
            Statements = statements;
        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
