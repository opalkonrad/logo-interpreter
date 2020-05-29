namespace LogoInterpreter.Interpreter
{
    public class LessEqualThanToken : Token
    {
        public static string Text = "<=";

        public LessEqualThanToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
