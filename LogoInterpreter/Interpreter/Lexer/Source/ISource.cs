using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    interface ISource
    {
        Position Position { get; }
        char CurrChar { get; }

        void MoveToNextChar();
    }
}
