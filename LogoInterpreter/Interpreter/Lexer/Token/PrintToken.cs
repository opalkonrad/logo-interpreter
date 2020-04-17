using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class PrintToken : Token
    {
        public static string Text = "input";

        public PrintToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
