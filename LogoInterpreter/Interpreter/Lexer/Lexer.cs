using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Lexer
    {
        private string text;
        private int currPos;
        private bool interpret = true;
        private Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>()
        {
            { "num", TokenType.TYPE },
            { "str", TokenType.TYPE },
            { "if", TokenType.IF },
            { "else", TokenType.ELSE },
            { "repeat", TokenType.REPEAT },
            { "func", TokenType.FUNC },
            { "new", TokenType.NEW },
            { "Turtle", TokenType.TURTLE },
            { "input", TokenType.INPUT },
            { "print", TokenType.PRINT }
        };

        public Lexer()
        {

        }

        public Lexer(string text)
        {
            this.text = text;
        }

        public string Text
        {
            set { this.text = value; }
        }

        public Token NextToken()
        {
            skipWhitespace();

            if (currPos >= text.Length)
            {
                return new Token(TokenType.EOF, "EOF");
            }

            switch (text[currPos])
            {
                case '(':
                    return new Token(TokenType.LROUNDBRACKET, text[currPos++].ToString());

                case ')':
                    return new Token(TokenType.RROUNDBRACKET, text[currPos++].ToString());

                case '{':
                    return new Token(TokenType.LSQUAREBRACKET, text[currPos++].ToString());

                case '}':
                    return new Token(TokenType.RSQUAREBRACKET, text[currPos++].ToString());

                case '.':
                    return new Token(TokenType.DOT, text[currPos++].ToString());

                case ',':
                    return new Token(TokenType.COMMA, text[currPos++].ToString());

                case ';':
                    return new Token(TokenType.SEMICOLON, text[currPos++].ToString());

                case '+':
                case '-':
                    return new Token(TokenType.ADDOP, text[currPos++].ToString());

                case '*':
                case '/':
                    return new Token(TokenType.MULTOP, text[currPos++].ToString());

                case '=':
                    if (isNextCharEqual('='))
                    {
                        return new Token(TokenType.BOOLOP, text[currPos++].ToString() + text[currPos++].ToString());
                    }

                    return new Token(TokenType.ASSIGNOP, text[currPos++].ToString());

                case '!':
                case '<':
                case '>':
                    if (isNextCharEqual('='))
                    {
                        return new Token(TokenType.BOOLOP, text[currPos++].ToString() + text[currPos++].ToString());
                    }
                    else if (text[currPos] == '!')
                    {
                        // Impossible to have only '!'
                        // throw exception
                        return new Token(TokenType.ERR, "");
                    }

                    return new Token(TokenType.BOOLOP, text[currPos++].ToString());

                case '\'':
                    if (interpret)
                    {
                        interpret = false;
                    }
                    else
                    {
                        interpret = true;
                    }
                    
                    return new Token(TokenType.QUOTATION, text[currPos++].ToString());

                default:
                    if (char.IsLetter(text[currPos]) && interpret)
                    {
                        string ident = findIdentifier();
                        TokenType value;

                        if (keywords.TryGetValue(ident, out value))
                        {
                            return new Token(value, ident);
                        }

                        return new Token(TokenType.IDENTIFIER, ident);
                    }
                    else if (char.IsDigit(text[currPos]) && interpret)
                    {
                        string num = findNum();
                        return new Token(TokenType.NUMVAL, num);
                    }
                    else if (char.IsLetterOrDigit(text[currPos]) && !interpret)
                    {
                        string str = findStr();
                        return new Token(TokenType.STRVAL, str);
                    }
                    else
                    {
                        return new Token(TokenType.EOF, "EOF");
                    }
            }
        }

        public List<Token> Scan(string text)
        {
            this.text = text;
            currPos = 0;

            List<Token> tokens = new List<Token>();
            while (currPos != text.Length)
            {
                Token currToken = NextToken();
                tokens.Add(currToken);

                if (currToken.Val == "EOF")
                {
                    break;
                }
            }
            return tokens;
        }

        private void skipWhitespace()
        {
            while (currPos < text.Length && char.IsWhiteSpace(text[currPos]))
            {
                currPos++;
            }
        }

        private bool isNextCharEqual(char sign)
        {
            if (currPos < text.Length - 1 && text[currPos + 1] == sign)
            {
                return true;
            }

            return false;
        }

        private string findStr()
        {
            StringBuilder str = new StringBuilder();

            while (currPos < text.Length && text[currPos] != '\'')
            {
                str.Append(text[currPos]);
                currPos++;
            }

            return str.ToString();
        }

        private string findNum()
        {
            StringBuilder str = new StringBuilder();
            bool wasDot = false;

            while (currPos < text.Length && (char.IsDigit(text[currPos]) || (text[currPos] == '.' && !wasDot)))
            {
                if (text[currPos] == '.')
                {
                    wasDot = true;
                }

                str.Append(text[currPos]);
                currPos++;
            }

            return str.ToString();
        }

        private string findIdentifier()
        {
            StringBuilder ident = new StringBuilder();

            while (currPos < text.Length && char.IsLetterOrDigit(text[currPos]))
            {
                ident.Append(text[currPos]);
                currPos++;
            }

            return ident.ToString();
        }
    }
}
