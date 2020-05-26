using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    interface ITurtle
    {
        // Forward, backward, right, left
        void Forward(double value);
        void Backward(double value);
        void Right(double value);
        void Left(double value);

        // Pen up, pen down
        void PenUp();
        void PenDown();

        // Line color and thickness
        void LineColor(string value);
        void LineThickness(double value);
    }
}
