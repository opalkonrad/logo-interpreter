namespace LogoInterpreter.Interpreter
{
    public class TurtleToken : Token
    {
        public static string Text = "Turtle";

        public TurtleToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
