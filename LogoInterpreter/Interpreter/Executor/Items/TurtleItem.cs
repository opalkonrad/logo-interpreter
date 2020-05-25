using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogoInterpreter.Interpreter
{
    // Note that rotation transformations are counted counterclockwise and property Rotation is counted clockwise
    public class TurtleItem : Item, ITurtle
    {
        //public string LineColor { get; set; }
        //public double LineThickness { get; set; }
        public bool Draw { get; set; } // Pen up, pen down
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double Rotation { get; set; }

        private readonly Canvas canvas;
        private readonly Image turtleImg;

        public TurtleItem(string name, Canvas canvas)
            : base(name)
        {
            //LineColor = "Black";
            //LineThickness = Draw = 1;
            Draw = true;
            XPos = 0.0;
            YPos = 0.0;
            Rotation = 0.0;

            this.canvas = canvas;
            turtleImg = new Image();
            turtleImg.Source = new BitmapImage(new Uri("C:/Users/opalk/source/repos/LogoInterpreter/LogoInterpreter/Img/turtle.png"));
            turtleImg.Width = 32;
            turtleImg.Height = 32;

            placeTurtle();

            canvas.Children.Add(turtleImg);
        }

        public void Fd(double value)
        {
            if (Draw)
            {
                Line line = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = XPos,
                    Y1 = YPos
                };

                // Calculate the new position in coordinate system
                XPos += Math.Sin(Rotation * Math.PI / 180) * value;
                YPos += Math.Cos(Rotation * Math.PI / 180) * value;

                placeTurtle();

                line.X2 = XPos;
                line.Y2 = YPos;

                canvas.Children.Add(line);
            }
            else
            {
                // Calculate the new position in coordinate system
                XPos += Math.Sin(Rotation * Math.PI / 180) * value;
                YPos += Math.Cos(Rotation * Math.PI / 180) * value;

                placeTurtle();
            }
        }

        public void Bk(double value)
        {
            if (Draw)
            {
                Line line = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = XPos,
                    Y1 = YPos
                };

                // Calculate the new position in coordinate system
                XPos -= Math.Sin(Rotation * Math.PI / 180) * value;
                YPos -= Math.Cos(Rotation * Math.PI / 180) * value;

                placeTurtle();

                line.X2 = XPos;
                line.Y2 = YPos;

                canvas.Children.Add(line);
            }
            else
            {
                // Calculate the new position in coordinate system
                XPos -= Math.Sin(Rotation * Math.PI / 180) * value;
                YPos -= Math.Cos(Rotation * Math.PI / 180) * value;

                placeTurtle();
            }
        }

        public void Rt(double value)
        {
            Rotation += value;

            placeTurtle();
        }

        public void Lt(double value)
        {
            Rotation -= value;

            placeTurtle();
        }

        public void PU()
        {
            Draw = false;
        }

        public void PD()
        {
            Draw = true;
        }

        private void placeTurtle()
        {
            // Translate turtle
            TranslateTransform translate = new TranslateTransform(XPos - 16, YPos - 16);
            turtleImg.RenderTransform = translate;

            // Rotate turtle
            RotateTransform rotate = new RotateTransform(180 + (-Rotation), XPos, YPos);
            turtleImg.RenderTransform = rotate;

            TransformGroup myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(translate);
            myTransformGroup.Children.Add(rotate);
            
            turtleImg.RenderTransform = myTransformGroup;
        }
    }
}
