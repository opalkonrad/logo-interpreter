using System;
using System.Collections.Generic;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Lexer
    {
        private readonly string text;
        private int currPos;

        public Lexer(string text)
        {
            this.text = text;
        }

        public Token NextToken()
        {
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
                case '!':
                case '<':
                case '>':
                    // TODO
                    break;

                default:
                    // TODO
                    break;
            }
        }
    }
}
