using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class EqualToken : Token
    {
        public EqualToken(Position position, string text)
            : base(position, text) { }
    }
}
