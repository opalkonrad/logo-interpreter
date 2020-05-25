using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Scope
    {
        public Dictionary<string, Item> Items { get; private set; }
        //public Stack<dynamic> Stack { get; private set; }

        public Scope()
        {
            Items = new Dictionary<string, Item>();
            //Stack = new Stack<dynamic>();
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

                default:
                    throw new EvaluateException(); // TODO
            }
        }

        public void AddVarDeclaration(Item item)
        {
            Items.Add(item.Name, item);
        }

        public Item GetVarValue(string name)
        {
            return Items[name];
        }

        public void AddVar(string name, dynamic item)
        {
            Items[name] = item;
        }

        /*public void PushToTheStack(dynamic value)
        {
            Stack.Push(value);
        }

        public dynamic PopFromTheStack()
        {
            return Stack.Pop();
        }*/
    }
}
