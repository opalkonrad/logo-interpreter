using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LogoInterpreter.Interpreter
{
    public abstract class Statement
    {
        public abstract void Execute(Environment environment);
    }
}
