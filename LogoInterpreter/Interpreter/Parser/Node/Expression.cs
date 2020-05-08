using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Expression : Node
    {
        public BoolExpression Left { get; set; }
        public BoolExpression Right { get; set; }
        public BoolOperator Operator { get; set; }
        
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }

        public enum BoolOperator
        {
            Null,
            // TODO Token !=
            EqualToken,
            LessThanToken,
            GreaterThanToken,
            LessEqualThanToken,
            GreaterEqualThanToken
        }
    }
}
