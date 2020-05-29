namespace LogoInterpreter.Interpreter
{
    public class EndOfTextToken : Token
    {
        public static string Text = "EOT";

        public EndOfTextToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
