using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public interface INode
    {
        abstract void Accept(IVisitor visitor);
    }
}
