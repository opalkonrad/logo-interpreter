using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    class IdentifierExprParam : INode
    {
        public bool Unary { get; set; }
        public string Value { get; set; }

        public IdentifierExprParam()
        {

        }

        public IdentifierExprParam(bool unary, string value)
        {
            Unary = unary;
            Value = value;
        }

        public void Accept(Visitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
