namespace LogoInterpreter.Interpreter
{
    public class DotToken : Token
    {
        public static string Text = ".";

        public DotToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
