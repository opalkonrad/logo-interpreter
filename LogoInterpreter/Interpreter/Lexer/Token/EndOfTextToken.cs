using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class EndOfTextToken : Token
    {
        public EndOfTextToken(Position position, string text)
            : base(position, text) { }
    }
}
