using System.Collections.Generic;

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
            return type switch
            {
                "StrToken" => AddItem(new StrItem(name)),
                "NumToken" => AddItem(new NumItem(name)),
                _ => false,
            };
        }

        public bool AddReferenceToTurtle(string refName, Item item)
        {
            return items.TryAdd(refName, item);
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
    }
}
