using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class NumValueExprParam : INode
    {
        public bool Unary { get; set; }
        public double Value { get; set; }

        public NumValueExprParam()
        {

        }

        public NumValueExprParam(bool unary, double value)
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
