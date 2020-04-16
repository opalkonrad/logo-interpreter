using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class NumToken : Token
    {
        public double Value { get; }

        public NumToken(Position position, string text, double value)
            : base (position, text)
        {
            Value = value;
        }
    }
}
