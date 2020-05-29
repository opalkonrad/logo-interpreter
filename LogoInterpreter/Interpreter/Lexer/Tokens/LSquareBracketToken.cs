namespace LogoInterpreter.Interpreter
{
    public class LSquareBracketToken : Token
    {
        public static string Text = "{";

        public LSquareBracketToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
