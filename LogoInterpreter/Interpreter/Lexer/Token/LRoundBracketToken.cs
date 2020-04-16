using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class LRoundBracketToken : Token
    {
        public LRoundBracketToken(Position position, string text)
            : base(position, text) { }
    }
}
