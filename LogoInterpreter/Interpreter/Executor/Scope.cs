using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Scope
    {
        public Dictionary<string, dynamic> Items { get; private set; }

        public Scope()
        {
            Items = new Dictionary<string, object>();
        }

        public void AddVarDeclaration(string name, string type)
        {
            switch (type)
            {
                case "StrToken":
                    Items.Add(name, new StrItem(name));
                    break;

                case "NumToken":
                    Items.Add(name, new NumItem(name));
                    break;

                case "TurtleToken":
                    Items.Add(name, new TurtleItem(name));
                    break;
            }
        }

        public dynamic GetVarValue(string name)
        {
            return Items[name];
        }
    }
}
