using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class IfStatement : INode
    {
        public EqualCondition Condition { get; set; }
        public BlockStatement Body { get; set; }
        public BlockStatement ElseBody { get; set; }
        
        public IfStatement()
        {

        }

        public IfStatement(EqualCondition cond, BlockStatement body, BlockStatement elseBody)
        {
            Condition = cond;
            Body = body;
            ElseBody = elseBody;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
