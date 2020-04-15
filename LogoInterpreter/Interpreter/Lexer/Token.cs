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

        public override string ToString()
        {
            return "TOKEN Type: " + Type + ", Value: " + Val;
        }
    }

    enum TokenType
    {
        [TokenCode("IDENTIFIER")] IDENTIFIER,
        [TokenCode("TYPE")] TYPE,
        [TokenCode("NUMVAL")] NUMVAL,
        [TokenCode("STRVAL")] STRVAL,

        [TokenCode("IF")] IF,
        [TokenCode("ELSE")] ELSE,
        [TokenCode("REPEAT")] REPEAT,
        [TokenCode("FUNC")] FUNC,
        [TokenCode("NEW")] NEW,
        [TokenCode("TURTLE")] TURTLE,
        [TokenCode("INPUT")] INPUT,
        [TokenCode("PRINT")] PRINT,

        [TokenCode("(")] LROUNDBRACKET,
        [TokenCode(")")] RROUNDBRACKET,
        [TokenCode("{")] LSQUAREBRACKET,
        [TokenCode("}")] RSQUAREBRACKET,

        [TokenCode(".")] DOT,
        [TokenCode(",")] COMMA,
        [TokenCode(";")] SEMICOLON,
        [TokenCode("'")] QUOTATION,

        [TokenCode("=")] ASSIGNOP,
        [TokenCode("")] BOOLOP,
        [TokenCode("")] ADDOP,
        [TokenCode("")] MULTOP,

        [TokenCode("EOF")] EOF,
        [TokenCode("ERR")] ERR
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
