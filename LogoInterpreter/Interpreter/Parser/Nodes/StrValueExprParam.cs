namespace LogoInterpreter.Interpreter
{
    public class StrValueExprParam : INode
    {
        public string Value { get; set; }

        public StrValueExprParam()
        {

        }

        public StrValueExprParam(string value)
        {
            Value = value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
