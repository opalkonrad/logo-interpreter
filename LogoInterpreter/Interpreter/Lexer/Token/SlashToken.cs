using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class SlashToken : Token
    {
        public SlashToken(Position position, string text)
            : base(position, text) { }
    }
}
