using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class AsteriskToken : Token
    {
        public AsteriskToken(Position position, string text)
            : base(position, text) { }
    }
}
