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

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
