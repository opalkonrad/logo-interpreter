namespace LogoInterpreter.Interpreter
{
    public class StringSource : ISource
    {
        public Position Position { get; set; } = new Position();
        public char CurrChar { get; set; }

        private readonly string str;

        public StringSource(string str)
        {
            this.str = str.Replace("\r\n", "\n");
        }

        public void MoveToNextChar()
        {
            // In comparison to string.Length, Position.Char doesn't include new lines
            if ((Position.Char + (Position.Line - 1)) >= str.Length)
            {
                CurrChar = (char)3;
                setPosition();
                return;
            }

            CurrChar = str[Position.Char + (Position.Line - 1)];
            setPosition();
        }

        private void setPosition()
        {
            if (CurrChar == '\n')
            {
                Position.NextLine();
            }
            else
            {
                Position.NextColumn();
            }
        }
    }
}
