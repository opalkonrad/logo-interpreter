using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class AssignmentStatement : INode
    {
        public string Variable { get; set; }
        public AddExpression RightSideExpression { get; set; }

        public AssignmentStatement()
        {

        }

        public AssignmentStatement(string var, AddExpression rightSide)
        {
            Variable = var;
            RightSideExpression = rightSide;
        }

        public void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
