namespace LogoInterpreter.Interpreter
{
    public class NotEqualToken : Token
    {
        public static string Text = "!=";

        public NotEqualToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
