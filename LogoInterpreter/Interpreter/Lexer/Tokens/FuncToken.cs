namespace LogoInterpreter.Interpreter
{
    public class FuncToken : Token
    {
        public static string Text = "func";

        public FuncToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
