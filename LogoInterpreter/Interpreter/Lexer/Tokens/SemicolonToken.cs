namespace LogoInterpreter.Interpreter
{
    public class SemicolonToken : Token
    {
        public static string Text = ";";

        public SemicolonToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
