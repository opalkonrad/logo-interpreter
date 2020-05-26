using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Scope
    {
        private readonly Dictionary<string, Item> items;

        public Scope()
        {
            items = new Dictionary<string, Item>();
        }

        public bool AddVarDeclaration(string name, string type)
        {
            switch (type)
            {
                case "StrToken":
                    items.Add(name, new StrItem(name));
                    return true;

                case "NumToken":
                    items.Add(name, new NumItem(name));
                    return true;

                default:
                    return false;
            }
        }

        public bool AddItem(Item item)
        {
            return items.TryAdd(item.Name, item);
        }

        public Item GetItem(string name)
        {
            if (items.TryGetValue(name, out Item value))
            {
                return value;
            }

            return null;
        }

        public bool Contains(string name)
        {
            return items.ContainsKey(name);
        }

        public bool AddReferenceToTurtle(string refName, Item item)
        {
            return items.TryAdd(refName, item);
        }
    }
}
