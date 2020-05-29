namespace LogoInterpreter.Interpreter
{
    public class GreaterEqualThanToken : Token
    {
        public static string Text = ">=";

        public GreaterEqualThanToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
