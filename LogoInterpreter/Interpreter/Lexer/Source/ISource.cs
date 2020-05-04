using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public interface ISource
    {
        Position Position { get; }
        char CurrChar { get; }

        void MoveToNextChar();
    }
}
