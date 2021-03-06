﻿using System.Collections.Generic;

namespace LogoInterpreter.Interpreter
{
    public class EqualCondition : INode
    {
        public List<RelationalCondition> Operands { get; set; } = new List<RelationalCondition>();
        public List<string> Operators { get; set; } = new List<string>();

        public EqualCondition()
        {

        }

        public EqualCondition(List<RelationalCondition> operands, List<string> operators)
        {
            Operands = operands;
            Operators = operators;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
