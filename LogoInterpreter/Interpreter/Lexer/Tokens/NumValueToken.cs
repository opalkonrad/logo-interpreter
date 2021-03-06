﻿namespace LogoInterpreter.Interpreter
{
    public class NumValueToken : Token
    {
        public double Value { get; }

        public NumValueToken(Position position, double value)
            : base (position)
        {
            Value = value;
        }

        public override string ToString()
        {
            return base.ToString() + ", Value: " + Value;
        }
    }
}
