namespace LogoInterpreter.Interpreter
{
    public class RepeatToken : Token
    {
        public static string Text = "repeat";

        public RepeatToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
