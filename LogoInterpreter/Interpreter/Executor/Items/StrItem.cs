using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class StrItem
    {
        public string Name { get; }
        public string Value { get; set; }

        public StrItem(string name)
        {
            Name = name;
        }

        public StrItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
