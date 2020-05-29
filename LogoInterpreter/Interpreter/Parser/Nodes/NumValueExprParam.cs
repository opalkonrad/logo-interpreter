namespace LogoInterpreter.Interpreter
{
    public class NumValueExprParam : INode
    {
        public bool Unary { get; set; }
        public double Value { get; set; }

        public NumValueExprParam()
        {

        }

        public NumValueExprParam(bool unary, double value)
        {
            Unary = unary;
            Value = value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
