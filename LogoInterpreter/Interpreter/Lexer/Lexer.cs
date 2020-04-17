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
        private static string[] keywords = new string[]
        {
            "if", "else"
        };

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

            if (buildBracket())
            {
                return;
            }

            if (buildDotCommaSemicolon())
            {
                return;
            }

            if (buildIdentifierOrKeyword())
            {
                return;
            }

            if (buildStrValue())
            {
                return;
            }

            if (buildNumValue())
            {
                return;
            }

            if (buildOperatorOrAssignment())
            {
                return;
            }

            throw new LexerException("Wrong token in " + source.Position.ToString());
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
                Token = new EndOfTextToken(new Position(source.Position));
                return true;
            }

            return false;
        }

        private bool buildBracket()
        {
            if (new[] { '{', '}', '(', ')' }.Contains(source.CurrChar))
            {
                switch (source.CurrChar)
                {
                    case '{':
                        Token = new LSquareBracketToken(new Position(source.Position));
                        break;

                    case '}':
                        Token = new RSquareBracketToken(new Position(source.Position));
                        break;

                    case '(':
                        Token = new LRoundBracketToken(new Position(source.Position));
                        break;

                    case ')':
                        Token = new RRoundBracketToken(new Position(source.Position));
                        break;
                }

                source.MoveToNextChar();
                return true;
            }

            return false;
        }

        private bool buildDotCommaSemicolon()
        {
            if (new[] { '.', ',', ';' }.Contains(source.CurrChar))
            {
                switch (source.CurrChar)
                {
                    case '.':
                        Token = new DotToken(new Position(source.Position));
                        break;

                    case ',':
                        Token = new CommaToken(new Position(source.Position));
                        break;

                    case ';':
                        Token = new SemicolonToken(new Position(source.Position));
                        break;
                }

                source.MoveToNextChar();
                return true;
            }

            return false;
        }

        private bool buildIdentifierOrKeyword()
        {
            if (char.IsLetter(source.CurrChar))
            {
                Position beginPosition = new Position(source.Position);
                StringBuilder strVal = new StringBuilder();

                while (char.IsLetter(source.CurrChar))
                {
                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }
                    
                switch(strVal.ToString())
                {
                    case "if":
                        Token = new IfToken(new Position(beginPosition));
                        break;

                    case "else":
                        Token = new ElseToken(new Position(beginPosition));
                        break;

                    case "repeat":
                        Token = new RepeatToken(new Position(beginPosition));
                        break;

                    case "num":
                        Token = new NumToken(new Position(beginPosition));
                        break;

                    case "str":
                        Token = new StrToken(new Position(beginPosition));
                        break;

                    case "func":
                        Token = new FuncToken(new Position(beginPosition));
                        break;

                    case "new":
                        Token = new NewToken(new Position(beginPosition));
                        break;

                    case "Turtle":
                        Token = new TurtleToken(new Position(beginPosition));
                        break;

                    case "input":
                        Token = new InputToken(new Position(beginPosition));
                        break;

                    case "print":
                        Token = new PrintToken(new Position(beginPosition));
                        break;

                    default:
                        Token = new IdentifierToken(new Position(beginPosition), strVal.ToString());
                        break;
                }
                
                return true;
            }

            return false;
        }

        private bool buildStrValue()
        {
            if (source.CurrChar == '\"')
            {
                Position beginPosition = new Position(source.Position);
                StringBuilder strVal = new StringBuilder();

                // Skip quotation mark
                source.MoveToNextChar();

                while (source.CurrChar != '\"')
                {
                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }

                // Skip quotation mark
                source.MoveToNextChar();

                // Check for empty string
                if (strVal.Length != 0)
                {
                    Token = new StrValueToken(new Position(beginPosition), strVal.ToString());
                }
                else
                {
                    Token = new StrValueToken(new Position(beginPosition), string.Empty);
                }
                
                return true;
            }

            return false;
        }

        private bool buildNumValue()
        {
            if (char.IsDigit(source.CurrChar))
            {
                Position beginPosition = new Position(source.Position);
                StringBuilder strVal = new StringBuilder();
                bool wasDot = false;

                while (char.IsDigit(source.CurrChar) || (source.CurrChar == '.' && !wasDot) )
                {
                    // Check for dot - only one for num value is possible
                    if (source.CurrChar == '.')
                    {
                        wasDot = true;
                    }

                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }

                Token = new NumValueToken(new Position(beginPosition), Convert.ToDouble(strVal.ToString()));
                return true;
            }

            return false;
        }

        private bool buildOperatorOrAssignment()
        {
            // Only one-character tokens
            if (new[] { '+', '-', '*', '/' }.Contains(source.CurrChar))
            {
                switch(source.CurrChar)
                {
                    case '+':
                        Token = new PlusToken(new Position(source.Position));
                        break;

                    case '-':
                        Token = new MinusToken(new Position(source.Position));
                        break;

                    case '*':
                        Token = new AsteriskToken(new Position(source.Position));
                        break;

                    case '/':
                        Token = new SlashToken(new Position(source.Position));
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
                            Token = new LessEqualThanToken(new Position(beginPosition));
                            break;

                        case '>':
                            Token = new GreaterEqualThanToken(new Position(beginPosition));
                            break;

                        case '=':
                            Token = new EqualToken(new Position(beginPosition));
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
                            Token = new LessThanToken(new Position(beginPosition));
                            break;

                        case '>':
                            Token = new GreaterThanToken(new Position(beginPosition));
                            break;

                        case '=':
                            Token = new AssignmentToken(new Position(beginPosition));
                            break;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
