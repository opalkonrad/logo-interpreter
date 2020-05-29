using System;

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
