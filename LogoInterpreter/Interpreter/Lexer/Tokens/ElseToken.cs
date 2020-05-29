namespace LogoInterpreter.Interpreter
{
    public class ElseToken : Token
    {
        public static string Text = "else";

        public ElseToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
