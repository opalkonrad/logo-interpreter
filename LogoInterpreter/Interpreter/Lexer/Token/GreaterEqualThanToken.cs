using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class GreaterEqualThanToken : Token
    {
        public GreaterEqualThanToken(Position position, string text)
            : base(position, text) { }
    }
}
