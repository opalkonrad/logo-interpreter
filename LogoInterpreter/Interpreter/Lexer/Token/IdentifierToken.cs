using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class IdentifierToken : Token
    {
        public string Value { get; }

        public IdentifierToken(Position position, string value)
            : base(position)
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + ", Value: " + Value;
        }
    }
}
