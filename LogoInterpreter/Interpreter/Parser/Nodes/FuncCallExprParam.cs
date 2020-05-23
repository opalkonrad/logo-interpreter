using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class FuncCallExprParam : INode
    {
        public bool Unary { get; set; }
        public FuncCall FuncCall { get; set; }

        public FuncCallExprParam()
        {

        }

        public FuncCallExprParam(bool unary, FuncCall funcCall)
        {
            Unary = unary;
            FuncCall = funcCall;
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
