using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class NumValueToken : Token
    {
        public double Value { get; }

        public NumValueToken(Position position, double value)
            : base (position)
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + ", Value: " + Value;
        }
    }
}
