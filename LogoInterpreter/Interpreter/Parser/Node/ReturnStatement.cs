﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ReturnStatement : INode
    {
        public AddExpression Expression { get; set; }

        public ReturnStatement(AddExpression expr)
        {
            Expression = expr;
        }

        public void Accept(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}