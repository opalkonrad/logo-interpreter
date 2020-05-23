using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class FuncCall : INode
    {
        public string Name { get; set; }
        public List<AddExpression> Arguments { get; set; }

        public FuncCall()
        {

        }

        public FuncCall(string name, List<AddExpression> args)
        {
            Name = name;
            Arguments = args;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
