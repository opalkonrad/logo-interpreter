using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Parser
    {
        private Lexer lexer;
        private Environment environment = new Environment();

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public Program ParseProgram()
        {
            return new Program();
        }
    }
}
