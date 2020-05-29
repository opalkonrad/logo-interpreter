namespace LogoInterpreter.Interpreter
{
    public class SlashToken : Token
    {
        public static string Text = "/";

        public SlashToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
