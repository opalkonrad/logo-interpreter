using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    public class Position
    {
        public int Column { get; set; }
        public int Line { get; set; }
        public int Char { get; set; } // Character number in text

        public Position()
        {
            Column = 0;
            Line = 1;
            Char = 0;
        }

        public Position(int col, int ln, int ch)
        {
            Column = col;
            Line = ln;
            Char = ch;
        }

        public Position(Position otherPosition)
        {
            Column = otherPosition.Column;
            Line = otherPosition.Line;
            Char = otherPosition.Char;
        }

        public void NextLine()
        {
            Line++;
            Column = 0;
        }

        public void NextColumn()
        {
            Column++;
            Char++;
        }

        public override string ToString()
        {
            return "Position: col = " + Column + ", ln = " + Line + ", ch = " + Char;
        }
    }
}
