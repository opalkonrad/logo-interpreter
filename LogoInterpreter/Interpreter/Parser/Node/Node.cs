using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public abstract class Node
    {
        public abstract void Accept(Visitor visitor);
    }
}
