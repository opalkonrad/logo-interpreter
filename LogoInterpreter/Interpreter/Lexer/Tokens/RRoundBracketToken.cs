namespace LogoInterpreter.Interpreter
{
    public class RRoundBracketToken : Token
    {
        public static string Text = ")";

        public RRoundBracketToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
