using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class TurtleItem : Item
    {
        public string LineColor { get; set; }
        public double LineThickness { get; set; }
        public double Draw { get; set; }

        public TurtleItem(string name)
            : base(name)
        {
            LineColor = "Black";
            LineThickness = Draw = 1;
        }
    }
}
