using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace LogoInterpreter.Interpreter
{
    public class Lexer
    {
        public Token Token { get; private set; }

        private readonly ISource source;
        private Position beginPosition;

        public Lexer(ISource source)
        {
            this.source = source;
            source.MoveToNextChar();
        }

        public void NextToken()
        {
            // Skip whitespace
            skipWhitespace();

            // Begin position of token
            beginPosition = new Position(source.Position);

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
                Token = new EndOfTextToken(beginPosition);
                return true;
            }

            return false;
        }

        private bool buildBracket()
        {
            switch (source.CurrChar)
            {
                case '{':
                    Token = new LSquareBracketToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '}':
                    Token = new RSquareBracketToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '(':
                    Token = new LRoundBracketToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case ')':
                    Token = new RRoundBracketToken(beginPosition);
                    source.MoveToNextChar();
                    return true;
            }

            return false;
        }

        private bool buildDotCommaSemicolon()
        {
            switch (source.CurrChar)
            {
                case '.':
                    Token = new DotToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case ',':
                    Token = new CommaToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case ';':
                    Token = new SemicolonToken(beginPosition);
                    source.MoveToNextChar();
                    return true;
            }

            return false;
        }

        private bool buildIdentifierOrKeyword()
        {
            if (char.IsLetter(source.CurrChar))
            {
                StringBuilder strVal = new StringBuilder();

                while (char.IsLetterOrDigit(source.CurrChar))
                {
                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }

                // Check if exists in keywords, otherwise create new identifier
                Token = (strVal.ToString()) switch
                {
                    "if" => new IfToken(beginPosition),
                    "else" => new ElseToken(beginPosition),
                    "repeat" => new RepeatToken(beginPosition),
                    "num" => new NumToken(beginPosition),
                    "str" => new StrToken(beginPosition),
                    "func" => new FuncToken(beginPosition),
                    "return" => new ReturnToken(beginPosition),
                    "new" => new NewToken(beginPosition),
                    "Turtle" => new TurtleToken(beginPosition),
                    "input" => new InputToken(beginPosition),
                    "print" => new PrintToken(beginPosition),
                    "Red" => new RedToken(beginPosition),
                    "Green" => new GreenToken(beginPosition),
                    "Blue" => new BlueToken(beginPosition),
                    _ => new IdentifierToken(beginPosition, strVal.ToString()),
                };

                return true;
            }

            return false;
        }

        private bool buildStrValue()
        {
            if (source.CurrChar == '\"')
            {
                StringBuilder strVal = new StringBuilder();

                // Skip quotation mark
                source.MoveToNextChar();

                while (source.CurrChar != '\"')
                {
                    // No closing quote
                    if (source.CurrChar == (char)3)
                    {
                        throw new LexerException("No closing bracket for str value in " + beginPosition.ToString());
                    }

                    strVal.Append(source.CurrChar);
                    source.MoveToNextChar();
                }

                // Skip quotation mark
                source.MoveToNextChar();

                Token = new StrValueToken(beginPosition, strVal.ToString());
                
                return true;
            }

            return false;
        }

        private bool buildNumValue()
        {
            if (char.IsDigit(source.CurrChar))
            {
                StringBuilder strVal = new StringBuilder();

                if (source.CurrChar == '0')
                {
                    strVal.Append('0');
                    source.MoveToNextChar();

                    // After leading zero should be dot or nothing
                    if (char.IsLetterOrDigit(source.CurrChar))
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
                    if (!char.IsDigit(source.CurrChar))
                    {
                        throw new LexerException("Wrong format of num value in " + beginPosition.ToString());
                    }

                    while (char.IsDigit(source.CurrChar))
                    {
                        strVal.Append(source.CurrChar);
                        source.MoveToNextChar();
                    }
                }

                // Character after digit or dot is impossible
                if (char.IsLetter(source.CurrChar))
                {
                    throw new LexerException("Wrong format of num value in " + beginPosition.ToString());
                }

                Token = new NumValueToken(beginPosition, Convert.ToDouble(strVal.ToString()));

                return true;
            }

            return false;
        }

        private bool buildOperatorOrAssignment()
        {
            // Only one-character tokens
            switch (source.CurrChar)
            {
                case '+':
                    Token = new PlusToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '-':
                    Token = new MinusToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '*':
                    Token = new AsteriskToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '/':
                    Token = new SlashToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '&':
                    Token = new AndToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '|':
                    Token = new OrToken(beginPosition);
                    source.MoveToNextChar();
                    return true;

                case '<':
                    source.MoveToNextChar();

                    if (source.CurrChar == '=')
                    {
                        Token = new LessEqualThanToken(beginPosition);
                        source.MoveToNextChar();
                    }
                    else
                    {
                        Token = new LessThanToken(beginPosition);
                    }
                    return true;

                case '>':
                    source.MoveToNextChar();

                    if (source.CurrChar == '=')
                    {
                        Token = new GreaterEqualThanToken(beginPosition);
                        source.MoveToNextChar();
                    }
                    else
                    {
                        Token = new GreaterThanToken(beginPosition);
                    }
                    return true;

                case '=':
                    source.MoveToNextChar();

                    if (source.CurrChar == '=')
                    {
                        Token = new EqualToken(beginPosition);
                        source.MoveToNextChar();
                    }
                    else
                    {
                        Token = new AssignmentToken(beginPosition);
                    }
                    
                    return true;
            }

            return false;
        }
    }
}
