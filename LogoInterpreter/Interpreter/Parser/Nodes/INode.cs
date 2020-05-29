namespace LogoInterpreter.Interpreter
{
    public interface INode
    {
        abstract void Accept(IVisitor visitor);
    }
}
