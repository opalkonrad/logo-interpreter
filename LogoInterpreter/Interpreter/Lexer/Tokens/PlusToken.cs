namespace LogoInterpreter.Interpreter
{
    public class PlusToken : Token
    {
        public static string Text = "+";

        public PlusToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
