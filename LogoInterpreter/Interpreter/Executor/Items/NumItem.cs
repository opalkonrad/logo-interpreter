using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class NumItem : Item
    {
        public double Value { get; set; }

        public NumItem(string name)
            : base(name)
        {
            
        }

        public NumItem(string name, double value)
            : base(name)
        {
            Value = value;
        }
    }
}
