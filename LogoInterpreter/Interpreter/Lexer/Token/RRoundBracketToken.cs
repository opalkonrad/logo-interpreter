using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class RRoundBracketToken : Token
    {
        public RRoundBracketToken(Position position, string text)
            : base(position, text) { }
    }
}
