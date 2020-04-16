using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Token
    {
        public Position Position { get; }
        public string Text { get; }

        public Token(Position position, string text)
        {
            Position = position;
            Text = text;
        }

        public override string ToString()
        {
            return "TOKEN: Type: " + Text + ", " + Position;
        }
    }
}
