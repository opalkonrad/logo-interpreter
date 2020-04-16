using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class StrToken : Token
    {
        public double Value { get; }

        public StrToken(Position position, string text, double value)
            : base(position, text)
        {
            Value = value;
        }
    }
}
