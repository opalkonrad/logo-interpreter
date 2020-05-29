using System;

namespace LogoInterpreter.Interpreter
{
    class ExecutorException : Exception
    {
        public ExecutorException()
        {

        }

        public ExecutorException(string msg)
            : base("# Exception (Executor): " + msg)
        {

        }
    }
}
