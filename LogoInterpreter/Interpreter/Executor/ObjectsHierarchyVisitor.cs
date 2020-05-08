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
            throw new NotImplementedException();
        }

        public override void Visit(AssignmentStatement node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(BlockStatement node)
        {
            objectsHierarchy.Write("Block Statement:\n");

            foreach (Node stmt in node.Statements)
            {
                stmt.Accept(this);
            }

            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(BoolExpression node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(Expression node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(FuncCall node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(IfStatement node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(MethCall node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(MultExpression node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(Parameter node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(RepeatStatement node)
        {
            throw new NotImplementedException();
        }

        public override void Visit(VarDeclaration node)
        {
            objectsHierarchy.Write("Variable Declaration:\n");
            objectsHierarchy.Write($"Name:\n{node.Name}\nType:\n{node.Type}\n");
            objectsHierarchy.Write("-------------------------\n");
        }

        public override void Visit(FuncDefinition node)
        {
            objectsHierarchy.Write("Function Definition:\n");
            objectsHierarchy.Write($"Name:\n{node.Name}\n");

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
