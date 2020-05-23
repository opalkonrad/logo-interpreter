using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class NumItem
    {
        public string Name { get; }
        public double Value { get; set; }

        public NumItem(string name)
        {
            Name = name;
        }

        public NumItem(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
