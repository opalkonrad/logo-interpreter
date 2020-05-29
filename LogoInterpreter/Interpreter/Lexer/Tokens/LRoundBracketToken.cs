namespace LogoInterpreter.Interpreter
{
    public class LRoundBracketToken : Token
    {
        public static string Text = "(";

        public LRoundBracketToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
