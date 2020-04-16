using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class IfToken : Token
    {
        public IfToken(Position position, string text)
            : base(position, text) { }
    }
}
