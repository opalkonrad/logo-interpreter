using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class LessEqualThanToken : Token
    {
        public LessEqualThanToken(Position position, string text)
            : base(position, text) { }
    }
}
