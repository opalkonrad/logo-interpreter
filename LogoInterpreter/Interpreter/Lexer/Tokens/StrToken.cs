namespace LogoInterpreter.Interpreter
{
    public class StrToken : Token
    {
        public static string Text = "str";

        public StrToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
