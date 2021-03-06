﻿namespace LogoInterpreter.Interpreter
{
    public class StrValueToken : Token
    {
        public string Value { get; }

        public StrValueToken(Position position, string value)
            : base(position)
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + ", Value: " + Value;
        }
    }
}
