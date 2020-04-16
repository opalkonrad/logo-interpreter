using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class GreaterThanToken : Token
    {
        public GreaterThanToken(Position position, string text)
            : base(position, text) { }
    }
}
