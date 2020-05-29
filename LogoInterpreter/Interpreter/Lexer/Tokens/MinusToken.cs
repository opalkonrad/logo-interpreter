namespace LogoInterpreter.Interpreter
{
    public class MinusToken : Token
    {
        public static string Text = "-";

        public MinusToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
