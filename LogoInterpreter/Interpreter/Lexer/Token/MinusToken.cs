using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class MinusToken : Token
    {
        public MinusToken(Position position, string text)
            : base(position, text) { }
    }
}
