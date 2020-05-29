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

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
