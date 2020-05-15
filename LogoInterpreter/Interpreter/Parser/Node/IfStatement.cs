using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class IfStatement : INode
    {
        public AddExpression Condition { get; set; }
        public BlockStatement Body { get; set; }
        public BlockStatement ElseBody { get; set; }
        
        public IfStatement()
        {

        }

        public IfStatement(AddExpression cond, BlockStatement body, BlockStatement elseBody)
        {
            Condition = cond;
            Body = body;
            ElseBody = elseBody;
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
