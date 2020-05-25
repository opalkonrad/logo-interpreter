using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LogoInterpreter.Interpreter
{
    public class TurtleItem : Item, ITurtle
    {
        //public string LineColor { get; set; }
        //public double LineThickness { get; set; }
        public bool Draw { get; set; } = true; // Pen up, pen down
        public double XPos { get; set; }
        public double YPos { get; set; }

        private Canvas canvas;
        

        public TurtleItem(string name, Canvas canvas)
            : base(name)
        {
            //LineColor = "Black";
            //LineThickness = Draw = 1;
            XPos = 0.0;
            YPos = 0.0;
            this.canvas = canvas;
        }

        public void Fd(double value)
        {
            Line line = new Line();
            line.
            line.Stroke = System.Windows.Media.Brushes.Black;

            line.X1 = XPos;
            line.Y1 = YPos;

            // Increase XPos by value to move the turtle
            YPos += value;

            line.X2 = XPos;
            line.Y2 = YPos;

            canvas.Children.Add(line);
        }

        public void Bk(double value)
        {
            throw new NotImplementedException();
        }

        public void Rt(double value)
        {
            throw new NotImplementedException();
        }

        public void Lt(double value)
        {
            throw new NotImplementedException();
        }

        public void PU()
        {
            throw new NotImplementedException();
        }

        public void PD()
        {
            throw new NotImplementedException();
        }
    }
}
