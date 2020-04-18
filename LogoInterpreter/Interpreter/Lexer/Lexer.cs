using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    public class Lexer
    {
        public Token Token { get; private set; }

        private readonly ISource source;
        private static readonly char[] chars = new char[]
        {
            'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h',
            'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p',
            'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x',
            'Y', 'y', 'Z', 'z'
        };

        public Lexer(ISource source)
        {
            this.source = source;
            source.MoveToNextChar();
        }

        public void NextToken()
        {
            // Skip whitespace
            skipWhitespace();

            // Try to build token
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

            throw new LexerException("Didn't find appropriate token for text in " + source.Position.ToString());
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
            if (chars.Contains(source.CurrChar))
            {
                Position beginPosition = new Position(source.Position);
                StringBuilder strVal = new StringBuilder();

                while (chars.Contains(source.CurrChar) || char.IsDigit(source.CurrChar))
                {
                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }

                // Check if exists in keywords, otherwise create new identifier
                Token = (strVal.ToString()) switch
                {
                    "if" => new IfToken(new Position(beginPosition)),
                    "else" => new ElseToken(new Position(beginPosition)),
                    "repeat" => new RepeatToken(new Position(beginPosition)),
                    "num" => new NumToken(new Position(beginPosition)),
                    "str" => new StrToken(new Position(beginPosition)),
                    "func" => new FuncToken(new Position(beginPosition)),
                    "return" => new ReturnToken(new Position(beginPosition)),
                    "new" => new NewToken(new Position(beginPosition)),
                    "Turtle" => new TurtleToken(new Position(beginPosition)),
                    "input" => new InputToken(new Position(beginPosition)),
                    "print" => new PrintToken(new Position(beginPosition)),
                    "Red" => new RedToken(new Position(beginPosition)),
                    "Green" => new GreenToken(new Position(beginPosition)),
                    "Blue" => new BlueToken(new Position(beginPosition)),
                    _ => new IdentifierToken(new Position(beginPosition), strVal.ToString()),
                };

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

                if (source.CurrChar == '0')
                {
                    strVal.Append('0');
                    source.MoveToNextChar();

                    // After leading zero should be dot or nothing
                    if (char.IsDigit(source.CurrChar) || chars.Contains(source.CurrChar))
                    {
                        throw new LexerException("Wrong format of num value in " + beginPosition.ToString());
                    }
                }
                else
                {
                    // First digit can't be zero, then can be
                    do
                    {
                        strVal.Append(source.CurrChar);
                        source.MoveToNextChar();
                    } while (char.IsDigit(source.CurrChar));
                }

                // Fraction
                if (source.CurrChar == '.')
                {
                    // Add dot
                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();

                    // At least one digit after dot
                    bool wasDigit = false;

                    while (char.IsDigit(source.CurrChar))
                    {
                        strVal.Append(source.CurrChar);
                        source.MoveToNextChar();
                        wasDigit = true;
                    }
                    
                    if (!wasDigit)
                    {
                        throw new LexerException("Wrong format of num value in " + beginPosition.ToString());
                    }
                }

                // Character after digit or dot is impossible
                if (chars.Contains(source.CurrChar))
                {
                    throw new LexerException("Wrong format of num value in " + beginPosition.ToString());
                }

                Token = new NumValueToken(new Position(beginPosition), Convert.ToDouble(strVal.ToString()));
                return true;
            }

            return false;
        }

        private bool buildOperatorOrAssignment()
        {
            // Only one-character tokens
            if (new[] { '+', '-', '*', '/', '&', '|' }.Contains(source.CurrChar))
            {
                switch (source.CurrChar)
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

                    case '&':
                        Token = new AndToken(new Position(source.Position));
                        break;

                    case '|':
                        Token = new OrToken(new Position(source.Position));
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
                    // One-character tokens
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
