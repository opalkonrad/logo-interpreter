using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class GreaterEqualThanToken : Token
    {
        public static string Text = ">=";

        public GreaterEqualThanToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
