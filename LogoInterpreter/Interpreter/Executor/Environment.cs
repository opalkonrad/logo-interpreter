using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Environment
    {
        private readonly List<Scope> scopes;
        public Stack<dynamic> Stack { get; private set; }

        public Environment()
        {
            scopes = new List<Scope>();
            Stack = new Stack<dynamic>();
        }

        public void NewScope()
        {
            scopes.Add(new Scope());
        }

        public void DeleteScope()
        {
            scopes.Remove(scopes.Last());
        }

        public void AddVarDeclaration(string name, string type)
        {
            scopes.Last().AddVarDeclaration(name, type);
        }

        public void AddVarDeclaration(Item item)
        {
            scopes.Last().AddVarDeclaration(item);
        }

        public void AddVarValue(string name, dynamic item)
        {
            scopes.Last().AddVar(name, item);
        }

        public Item GetVarValue(string name)
        {
            return scopes.Last().GetVarValue(name);
        }

        /*public void PutOnTheStack(dynamic value)
        {
            scopes.Last().PushToTheStack(value);
        }*/

        public void PushToTheStack(dynamic value)
        {
            Stack.Push(value);
        }

        public dynamic PopFromTheStack()
        {
            return Stack.Pop();
        }
    }
}
