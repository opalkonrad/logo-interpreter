namespace LogoInterpreter.Interpreter
{
    public class GreaterThanToken : Token
    {
        public static string Text = ">";

        public GreaterThanToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
