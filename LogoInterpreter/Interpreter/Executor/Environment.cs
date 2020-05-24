using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Environment
    {
        private readonly List<Scope> scopes;

        public Environment()
        {
            scopes = new List<Scope>();
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

        public void AddVarValue(string name, dynamic item)
        {
            scopes.Last().AddVar(name, item);
        }

        public Item GetVarValue(string name)
        {
            return scopes.Last().GetVarValue(name);
        }

        public void PutOnTheStack(dynamic value)
        {
            scopes.Last().PushToTheStack(value);
        }

        public void PushToTheStack(dynamic value)
        {
            scopes.Last().PushToTheStack(value);
        }

        public dynamic PopFromTheStack()
        {
            return scopes.Last().PopFromTheStack();
        }
    }
}
