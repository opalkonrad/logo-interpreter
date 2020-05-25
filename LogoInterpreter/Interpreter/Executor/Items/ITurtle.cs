using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    interface ITurtle
    {
        // Forward, backward, right, left
        void Fd(double value);
        void Bk(double value);
        void Rt(double value);
        void Lt(double value);

        // Pen up, pen down
        void PU();
        void PD();
    }
}
