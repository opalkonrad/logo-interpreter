using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class AssignmentToken : Token
    {
        public AssignmentToken(Position position, string text)
            : base(position, text) { }
    }
}
