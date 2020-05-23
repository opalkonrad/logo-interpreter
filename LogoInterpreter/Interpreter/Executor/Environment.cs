using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Environment
    {
        private List<Scope> scopes;

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

        public dynamic GetVarValue(string name)
        {
            return scopes.Last().GetVarValue(name);
        }
    }
}
