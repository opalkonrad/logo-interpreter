namespace LogoInterpreter.Interpreter
{
    public class IfToken : Token
    {
        public static string Text = "if";

        public IfToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
