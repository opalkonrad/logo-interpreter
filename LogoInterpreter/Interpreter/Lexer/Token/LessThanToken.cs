using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class LessThanToken : Token
    {
        public LessThanToken(Position position, string text)
            : base(position, text) { }
    }
}
