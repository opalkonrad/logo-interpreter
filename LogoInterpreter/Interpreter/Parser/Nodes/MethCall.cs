﻿namespace LogoInterpreter.Interpreter
{
    public class MethCall : INode
    {
        public string TurtleName { get; set; }
        public string MethName { get; set; }
        public AddExpression Argument { get; set; }

        public MethCall()
        {

        }

        public MethCall(string turtleName, string methName, AddExpression arg)
        {
            TurtleName = turtleName;
            MethName = methName;
            Argument = arg;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
