using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class ErrorToken : Token
    {
        public string Text { get; }

        public ErrorToken(Position position, string text)
            : base(position)
        {
            Text = text;
        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
