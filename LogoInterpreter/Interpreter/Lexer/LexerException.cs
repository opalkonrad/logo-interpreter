using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    public class LexerException : Exception
    {
        public LexerException()
        {

        }

        public LexerException(string msg)
            : base("# Exception (Lexer): " + msg)
        {
            
        }
    }
}
