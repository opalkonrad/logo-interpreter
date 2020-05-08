﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class FuncCall : Node
    {
        public string Name { get; set; }
        public List<Expression> Arguments { get; set; }

        public FuncCall()
        {

        }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
