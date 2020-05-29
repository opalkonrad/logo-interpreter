namespace LogoInterpreter.Interpreter
{
    public class EqualToken : Token
    {
        public static string Text = "==";

        public EqualToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
