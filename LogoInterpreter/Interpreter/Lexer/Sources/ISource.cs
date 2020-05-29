namespace LogoInterpreter.Interpreter
{
    public interface ISource
    {
        Position Position { get; set; }
        char CurrChar { get; set; }

        void MoveToNextChar();
    }
}
