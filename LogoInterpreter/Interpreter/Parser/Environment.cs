using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Environment
    {
        public Dictionary<string, double?> Nums { get; private set; } = new Dictionary<string, double?>();
        public Dictionary<string, string> Strs { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, Turtle> Turtles { get; private set; } = new Dictionary<string, Turtle>();

        public Environment()
        {

        }

        public void DeclareNewVar(string varName, string varType)
        {
            if (Nums.ContainsKey(varName) || Strs.ContainsKey(varName) || Turtles.ContainsKey(varName))
            {
                throw new ParserException($"Variable \"{varName}\" is already defined in this scope");
            }

            switch (varType)
            {
                case "double":
                    Nums.Add(varName, null);
                    break;

                case "string":
                    Strs.Add(varName, null);
                    break;

                case "Turtle":
                    Turtles.Add(varName, new Turtle());
                    break;

                default:
                    throw new ParserException("Wrong type of variable");
            }
        }

        public void CreateLocalScope()
        {

        }

        public void DestroyLocalScope()
        {

        }
    }
}
