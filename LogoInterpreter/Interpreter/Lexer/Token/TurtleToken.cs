using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class TurtleToken : Token
    {
        public static string Text = "Turtle";

        public TurtleToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
