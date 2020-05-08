using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class BoolExpression : Node
    {
        public AddExpression Left { get; set; }
        public AddExpression Right { get; set; }
        public AddOperator Operator { get; set; }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }

        public enum AddOperator
        {
            Null,
            PlusToken,
            MinusToken,
            OrToken
        }
    }
}
