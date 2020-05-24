using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class StrItem : Item
    {
        public string Value { get; set; }

        public StrItem(string name)
            : base(name)
        { 
            
        }

        public StrItem(string name, string value)
            : base(name)
        {
            Value = value;
        }
    }
}
