using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Lexer
    {
        private readonly string text;
        private int currPos;
        private bool interpret = true;

        public Lexer(string text)
        {
            this.text = text;
        }

        public Token NextToken()
        {
            skipWhitespace();

            switch (text[currPos])
            {
                case '(':
                    currPos++;
                    return new Token(TokenType.LROUNDBRACKET, "(");

                case ')':
                    currPos++;
                    return new Token(TokenType.RROUNDBRACKET, ")");

                case '{':
                    currPos++;
                    return new Token(TokenType.LSQUAREBRACKET, "{");

                case '}':
                    currPos++;
                    return new Token(TokenType.RSQUAREBRACKET, "}");

                case '.':
                    currPos++;
                    return new Token(TokenType.DOT, ".");

                case ',':
                    currPos++;
                    return new Token(TokenType.COMMA, ",");

                case '+':
                    currPos++;
                    return new Token(TokenType.PLUS, "+");

                case '-':
                    currPos++;
                    return new Token(TokenType.MINUS, "-");

                case '*':
                    currPos++;
                    return new Token(TokenType.ASTERISK, "*");

                case '/':
                    currPos++;
                    return new Token(TokenType.SLASH, "/");

                case '=':
                    if (isNextCharEqual('='))
                    {
                        currPos += 2;
                        return new Token(TokenType.DOUBLEEQUAL, "==");
                    }

                    currPos++;
                    return new Token(TokenType.EQUAL, "=");

                case '!':
                    if (isNextCharEqual('='))
                    {
                        currPos += 2;
                        return new Token(TokenType.NOTEQUAL, "!=");
                    }

                    currPos++;
                    return new Token(TokenType.ERR, "ERR");

                case '<':
                    if (isNextCharEqual('='))
                    {
                        currPos += 2;
                        return new Token(TokenType.LESSEQUAL, "<=");
                    }

                    currPos++;
                    return new Token(TokenType.LESS, "<");

                case '>':
                    if (isNextCharEqual('='))
                    {
                        currPos += 2;
                        return new Token(TokenType.GREATEREQUAL, ">=");
                    }

                    currPos++;
                    return new Token(TokenType.GREATER, ">");

                case '\'':
                    currPos++;

                    if (interpret)
                    {
                        interpret = false;
                    }
                    else
                    {
                        interpret = true;
                    }
                    
                    return new Token(TokenType.QUOTATION, "\'");

                default:
                    if (char.IsLetter(text[currPos]))
                    {
                        if (interpret)
                        {
                            string ident = findIdentifier();
                            return new Token(TokenType.IDENTIFIER, ident);
                        }
                        else
                        {
                            string str = findStr();
                            return new Token(TokenType.STR, str);
                        }
                    }
                    else if (char.IsDigit(text[currPos]))
                    {
                        if (interpret)
                        {
                            string num = findIdentifier();
                            return new Token(TokenType.NUM, num);
                        }
                        else
                        {
                            string str = findStr();
                            return new Token(TokenType.STR, str);
                        }
                    }
                    else
                    {
                        // Return exception
                        break;
                    }
            }
            return new Token(TokenType.EOF, "EOF");
        }

        private void skipWhitespace()
        {
            while (char.IsWhiteSpace(text[currPos]))
            {
                currPos++;
            }
        }

        private bool isNextCharEqual(char sign)
        {
            if (text[currPos + 1] == sign)
            {
                return true;
            }

            return false;
        }

        private string findStr()
        {
            StringBuilder str = new StringBuilder();

            while (!isNextCharEqual('\''))
            {
                str.Append(text[currPos]);
                currPos++;
            }

            return str.ToString();
        }

        private string findIdentifier()
        {
            StringBuilder ident = new StringBuilder();

            while (!char.IsWhiteSpace(text[currPos]))
            {
                ident.Append(text[currPos]);
                currPos++;
            }

            return ident.ToString();
        }
    }
}
