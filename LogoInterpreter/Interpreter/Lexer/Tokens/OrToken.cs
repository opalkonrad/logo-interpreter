namespace LogoInterpreter.Interpreter
{
    public class OrToken : Token
    {
        public static string Text = "|";

        public OrToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
