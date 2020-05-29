namespace LogoInterpreter.Interpreter
{
    public class LessThanToken : Token
    {
        public static string Text = "<";

        public LessThanToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
