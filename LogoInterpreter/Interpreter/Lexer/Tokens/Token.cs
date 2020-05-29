namespace LogoInterpreter.Interpreter
{
    public class Token
    {
        public Position Position { get; }

        public Token(Position position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return "TOKEN: Type: " + this.GetType().Name + ", " + Position;
        }
    }
}
