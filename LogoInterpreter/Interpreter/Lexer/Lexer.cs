using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Lexer
    {
        public Token Token { get; private set; }

        private ISource source;

        public Lexer(ISource source)
        {
            this.source = source;
            source.MoveToNextChar();
        }

        public void NextToken()
        {
            skipWhitespace();

            if (buildEndOfText())
            {
                return;
            }

            /*if (buildIdentifierOrKeyword())
            {
                return;
            }*/

            if (buildOperatorOrAssignment())
            {
                return;
            }
        }

        private void skipWhitespace()
        {
            while (char.IsWhiteSpace(source.CurrChar))
            {
                source.MoveToNextChar();
            }
        }

        private bool buildEndOfText()
        {
            if (source.CurrChar == (char)3)
            {
                Token = new EndOfTextToken(new Position(source.Position), "EOT");
                return true;
            }

            return false;
        }

        private bool buildIdentifierOrKeyword()
        {
            return true;
        }

        private bool buildOperatorOrAssignment()
        {
            // Only one-character tokens
            if (new[] { '+', '-', '*', '/' }.Contains(source.CurrChar))
            {
                switch(source.CurrChar)
                {
                    case '+':
                        Token = new PlusToken(new Position(source.Position), "+");
                        break;

                    case '-':
                        Token = new MinusToken(new Position(source.Position), "-");
                        break;

                    case '*':
                        Token = new AsteriskToken(new Position(source.Position), "*");
                        break;

                    case '/':
                        Token = new SlashToken(new Position(source.Position), "/");
                        break;
                }

                source.MoveToNextChar();
                return true;
            }

            // One/two-character tokens
            if (new[] { '<', '>', '=' }.Contains(source.CurrChar))
            {
                // Remeber beginning of string
                Position beginPosition = new Position(source.Position);
                char beginChar = source.CurrChar;
                
                source.MoveToNextChar();

                // Check if the next character is '=' - two-character token
                if (source.CurrChar == '=')
                {
                    switch (beginChar)
                    {
                        case '<':
                            Token = new LessEqualThanToken(new Position(beginPosition), "<=");
                            break;

                        case '>':
                            Token = new GreaterEqualThanToken(new Position(beginPosition), ">=");
                            break;

                        case '=':
                            Token = new EqualToken(new Position(beginPosition), "==");
                            break;
                    }

                    // Go to next character
                    source.MoveToNextChar();
                }
                else
                {
                    switch (beginChar)
                    {
                        case '<':
                            Token = new LessThanToken(new Position(beginPosition), "<");
                            break;

                        case '>':
                            Token = new GreaterThanToken(new Position(beginPosition), ">");
                            break;

                        case '=':
                            Token = new AssignmentToken(new Position(beginPosition), "=");
                            break;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
