namespace LogoInterpreter.Interpreter
{
    public class AndToken : Token
    {
        public static string Text = "&";

        public AndToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
