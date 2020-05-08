using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    class ObjectsHierarchyVisitor : Visitor
    {
        private StreamWriter objectsHierarchy;

        public ObjectsHierarchyVisitor(Program program)
            : base(program)
        {

        }

        public void ShowObjectsHierarchy()
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "Text|*.txt";
            saveFile.FileName = "objectsHierarchy";

            if (saveFile.ShowDialog() == true)
            {
                // Create stream to write results
                objectsHierarchy = new StreamWriter(Path.GetFullPath(saveFile.FileName));

                foreach (FuncDefinition funcDef in program.FuncDefinitions)
                {
                    funcDef.Accept(this);
                }

                foreach (Node stmt in program.Statements)
                {
                    stmt.Accept(this);
                }

                if (objectsHierarchy != null)
                {
                    objectsHierarchy.Close();
                }
            }
        }

        public override void Visit(AddExpression node)
        {
            objectsHierarchy.Write("# Additive Expression:\n");

            objectsHierarchy.Write($"Operator:\n{node.Operator}\n");

            objectsHierarchy.Write($"Left Side Expression:\n");
            node.Left.Accept(this);

            objectsHierarchy.Write($"Right Side Expression:\n");
            node.Right?.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(AssignmentStatement node)
        {
            objectsHierarchy.Write("# Assignment Statement:\n");

            objectsHierarchy.Write($"Name:\n{node.Variable}\n");

            objectsHierarchy.Write("Right Side Expression:\n");
            node.RightSideExpression.Accept(this);            

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(BlockStatement node)
        {
            objectsHierarchy.Write("# Block Statement:\n");

            foreach (Node stmt in node.Statements)
            {
                stmt.Accept(this);
            }

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(BoolExpression node)
        {
            objectsHierarchy.Write("# Bool Expression:\n");

            objectsHierarchy.Write($"Operator:\n{node.Operator}\n");

            objectsHierarchy.Write($"Left Side Expression:\n");
            node.Left.Accept(this);

            objectsHierarchy.Write($"Right Side Expression:\n");
            node.Right?.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(Expression node)
        {
            objectsHierarchy.Write("# Expression:\n");

            objectsHierarchy.Write($"Operator:\n{node.Operator}\n");

            objectsHierarchy.Write($"Left Side Expression:\n");
            node.Left.Accept(this);

            objectsHierarchy.Write($"Right Side Expression:\n");
            node.Right?.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(FuncCall node)
        {
            objectsHierarchy.Write("# Function Call:\n");

            objectsHierarchy.Write($"Name:\n{node.Name}\n");

            objectsHierarchy.Write("Arguments:\n");
            foreach (Expression argument in node.Arguments)
            {
                argument.Accept(this);
            }

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(IfStatement node)
        {
            objectsHierarchy.Write("# If Statement:\n");

            objectsHierarchy.Write($"Condition:\n{node.Condition}\n");

            objectsHierarchy.Write("Body:\n");
            node.Body.Accept(this);

            objectsHierarchy.Write("Else Body:\n");
            node.ElseBody?.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(MethCall node)
        {
            objectsHierarchy.Write("# Method Call:\n");

            objectsHierarchy.Write($"Name:\n{node.MethName}\n");

            objectsHierarchy.Write($"Turtle:\n{node.TurtleName}\n");

            objectsHierarchy.Write($"Argument:\n{node.Argument}\n");

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(MultExpression node)
        {
            objectsHierarchy.Write("# Multiplicative Expression:\n");

            // TODO

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(RepeatStatement node)
        {
            objectsHierarchy.Write("# Repeat Statement:\n");

            objectsHierarchy.Write("Number Of Times:\n");
            node.NumOfTimes.Accept(this);

            objectsHierarchy.Write("Body:\n");
            node.Body.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(VarDeclaration node)
        {
            objectsHierarchy.Write("# Variable Declaration:\n");

            objectsHierarchy.Write($"Name:\n{node.Name}\n");

            objectsHierarchy.Write($"Type:\n{node.Type}\n");

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(FuncDefinition node)
        {
            objectsHierarchy.Write("# Function Definition:\n");

            objectsHierarchy.Write($"Name:\n{node.Name}\n");

            objectsHierarchy.Write("Parameters:\n");
            foreach (VarDeclaration parameter in node.Parameters)
            {
                parameter.Accept(this);
            }

            objectsHierarchy.Write("Body:\n");
            node.Body.Accept(this);

            objectsHierarchy.Write("-------------------------\n");
        }
    }
}
