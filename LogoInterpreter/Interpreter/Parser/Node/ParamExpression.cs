using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class ParamExpression : Expression
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }



    public class NumParam : ParamExpression
    {
        public double Value { get; set; }
    }

    public class IdentifierParam : ParamExpression
    {
        public string Value { get; set; }
    }

    public class ExpressionParam : ParamExpression
    {
        public Expression Expression { get; set; }
    }

    public class FuncCallParam : ParamExpression
    {
        public FuncCall FuncCall { get; set; }
    }
}
