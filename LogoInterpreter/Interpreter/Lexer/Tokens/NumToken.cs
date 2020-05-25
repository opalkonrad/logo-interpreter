﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class NumToken : Token
    {
        public static string Text = "num";

        public NumToken(Position position)
            : base(position)
        {

        }

        public override string ToString()
        {
            return base.ToString() + ", Text: " + Text;
        }
    }
}