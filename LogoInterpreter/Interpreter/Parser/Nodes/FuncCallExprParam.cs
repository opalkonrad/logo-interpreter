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
            visitor.Visit(this);
        }
    }
}
