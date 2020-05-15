using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class RepeatStatement : INode
    {
        public AddExpression NumOfTimes { get; set; }
        public BlockStatement Body { get; set; }

        public RepeatStatement()
        {

        }

        public RepeatStatement(AddExpression numOfTimes, BlockStatement body)
        {
            NumOfTimes = numOfTimes;
            Body = body;
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
