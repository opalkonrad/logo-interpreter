using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class TurtleItem
    {
        public string Name { get; }
        public string LineColor { get; set; }
        public double LineThickness { get; set; }
        public double Draw { get; set; }

        public TurtleItem(string name)
        {
            Name = name;
            LineColor = "Black";
            LineThickness = Draw = 1;
        }
    }
}
