using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class RRoundBracketToken : Token
    {
        public static string Text = ")";

        public RRoundBracketToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
