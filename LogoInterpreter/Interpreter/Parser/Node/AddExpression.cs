using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace LogoInterpreter.Interpreter
{
    public class AddExpression : Node
    {
        public MultExpression Left { get; set; }
        public MultExpression Right { get; set; }
        public MultOperator Operator { get; set; }

        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }

        public enum MultOperator
        {
            Null,
            AsteriskToken = '*',
            SlashToken,
            AndToken
        }
    }
}
