using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class PlusToken : Token
    {
        public PlusToken(Position position, string text)
            : base(position, text) { }
    }
}
