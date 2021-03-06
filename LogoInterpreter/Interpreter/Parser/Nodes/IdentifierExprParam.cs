﻿namespace LogoInterpreter.Interpreter
{
    public class IdentifierExprParam : INode
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

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
