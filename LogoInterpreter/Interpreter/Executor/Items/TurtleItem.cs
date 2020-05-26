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
    public class TurtleItem : Item, ITurtle
    {
        public bool Draw { get; set; } // Pen up, pen down
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double Rotation { get; set; }
        public Brush TLineColor { get; set; }
        public double TLineThickness { get; set; }

        private readonly Canvas canvas;
        private readonly Image turtleImg;

        public TurtleItem(string name, Canvas canvas)
            : base(name)
        {
            Draw = true;
            XPos = 0.0;
            YPos = 0.0;
            Rotation = 0.0;
            TLineColor = Brushes.Black;
            TLineThickness = 1;

            this.canvas = canvas;
            turtleImg = new Image();
            turtleImg.Source = new BitmapImage(new Uri("C:/Users/opalk/source/repos/LogoInterpreter/LogoInterpreter/Img/turtle.png"));
            turtleImg.Width = 32;
            turtleImg.Height = 32;

            placeTurtle();

            canvas.Children.Add(turtleImg);
        }

        public void Forward(double value)
        {
            if (Draw)
            {
                Line line = new Line
                {
                    Stroke = TLineColor,
                    StrokeThickness = TLineThickness,
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

        public void Backward(double value)
        {
            if (Draw)
            {
                Line line = new Line
                {
                    Stroke = TLineColor,
                    StrokeThickness = TLineThickness,
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

        public void Right(double value)
        {
            Rotation += value;

            placeTurtle();
        }

        public void Left(double value)
        {
            Rotation -= value;

            placeTurtle();
        }

        public void PenUp()
        {
            Draw = false;
        }

        public void PenDown()
        {
            Draw = true;
        }

        public void LineColor(string value)
        {
            switch (value)
            {
                case "Black":
                    TLineColor = Brushes.Black;
                    break;

                case "Red":
                    TLineColor = Brushes.Red;
                    break;

                case "Green":
                    TLineColor = Brushes.Green;
                    break;

                case "Blue":
                    TLineColor = Brushes.Blue;
                    break;
            }
        }

        public void LineThickness(double value)
        {
            if (value > 0 && value < 6)
            {
                TLineThickness = (int)value;
            }
        }

        // Note that rotation transformations are counted counterclockwise and property Rotation is counted clockwise
        private void placeTurtle()
        {
            // Translate turtle
            TranslateTransform translate = new TranslateTransform(XPos - 16, YPos - 16);
            turtleImg.RenderTransform = translate;

            // Rotate turtle
            RotateTransform rotate = new RotateTransform(180 + (-Rotation), XPos, YPos);
            turtleImg.RenderTransform = rotate;

            TransformGroup transformations = new TransformGroup();
            transformations.Children.Add(translate);
            transformations.Children.Add(rotate);
            
            turtleImg.RenderTransform = transformations;
        }
    }
}
