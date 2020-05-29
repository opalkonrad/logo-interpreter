namespace LogoInterpreter.Interpreter
{
    public class AssignmentToken : Token
    {
        public static string Text = "=";

        public AssignmentToken(Position position)
            : base(position)
        {
            
        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}
