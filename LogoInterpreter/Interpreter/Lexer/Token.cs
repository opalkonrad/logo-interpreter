using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LogoInterpreter.Interpreter.Lexer
{
    class Token
    {
        private readonly TokenType type;
        private readonly string val;

        public Token(TokenType type, string val)
        {
            this.type = type;
            this.val = val;
        }

        public TokenType Type
        {
            get { return type; }
        }

        public string Val
        {
            get { return val; }
        }
    }

    enum TokenType
    {
        [TokenCode("IDENTIFIER")] IDENTIFIER,
        [TokenCode("NUM")] NUM,
        [TokenCode("STR")] STR,
        
        [TokenCode("IF")] IF,
        [TokenCode("ELSE")] ELSE,
        [TokenCode("REPEAT")] REPEAT,
        [TokenCode("FUNC")] FUNC,

        [TokenCode("(")] LROUNDBRACKET,
        [TokenCode(")")] RROUNDBRACKET,
        [TokenCode("{")] LSQUAREBRACKET,
        [TokenCode("}")] RSQUAREBRACKET,

        [TokenCode("NEW")] NEW,
        [TokenCode("TURTLE")] TURTLE,

        [TokenCode(".")] DOT,
        [TokenCode(",")] COMMA,

        [TokenCode("=")] EQUAL,
        [TokenCode("!=")] NOTEQUAL,

        [TokenCode("==")] DOUBLEEQUAL,
        [TokenCode("<")] LESS,
        [TokenCode(">")] GREATER,
        [TokenCode("<=")] LESSEQUAL,
        [TokenCode(">=")] GREATEREQUAL,

        [TokenCode("+")] PLUS,
        [TokenCode("-")] MINUS,
        [TokenCode("|")] OR,

        [TokenCode("*")] ASTERISK,
        [TokenCode("/")] SLASH,
        [TokenCode("&")] AND,

        [TokenCode("EOF")] EOF
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class TokenCodeAttribute : Attribute
    {
        private readonly string tokenCode;

        public TokenCodeAttribute(string val)
        {
            this.tokenCode = val;
        }

        public string TokenCode
        {
            get { return tokenCode; }
        }
    }
}
