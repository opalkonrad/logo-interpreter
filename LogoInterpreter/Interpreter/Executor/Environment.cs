using System.Collections.Generic;
using System.Linq;

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
            if (!scopes.Last().AddVarDeclaration(name, type))
            {
                throw new ExecutorException($"Item with the name: {name} already exists");
            }
        }

        public void AddReferenceToTurtle(string name, TurtleItem item)
        {
            if (!scopes.Last().AddReferenceToTurtle(name, item))
            {
                throw new ExecutorException($"Turtle with the reference name: {name} referencing Turtle with the name: {item.Name} already exists");
            }
        }

        public void AddItem(Item item)
        {
            if (!scopes.Last().AddItem(item))
            {
                throw new ExecutorException($"Item with the name: {item.Name} already exists");
            }
        }

        public Item GetItem(string name)
        {
            // Find item in current scope and in higher scopes
            for (int scope = scopes.Count - 1; scope >= 0; scope--)
            {
                Item item = scopes[scope].GetItem(name);

                if (item != null)
                {
                    return item;
                }
            }

            throw new ExecutorException($"Item with the name: {name} does not exist");
        }

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
