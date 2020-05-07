using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public abstract class Visitor
    {
        public abstract void Visit(VarDeclaration node);
        public abstract void Visit(BlockStatement node);
        public abstract void Visit(Expression node);
        public abstract void Visit(RepeatStatement node);
        public abstract void Visit(FuncCall node);
        public abstract void Visit(IfStatement node);
        public abstract void Visit(MethCall node);
        public abstract void Visit(Parameter node);
    }
}
