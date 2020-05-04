using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    class ParserException : Exception
    {
        public ParserException()
        {

        }

        public ParserException(string msg)
            : base("# Exception (Parser): " + msg)
        {

        }
    }
}
