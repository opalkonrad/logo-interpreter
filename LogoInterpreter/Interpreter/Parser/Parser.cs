using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class Parser
    {
        private Lexer lexer;
        private Environment environment = new Environment();
        public Program program { get; private set; } = new Program();

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            lexer.NextToken();
        }

        private Token accept(Type type)
        {
            lexer.NextToken();

            if (lexer.Token.GetType() == type)
            {
                return lexer.Token;
            }

            throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected {type.Name}, got {lexer.Token.GetType().Name}");
        }

        private Token accept(Type[] types)
        {
            lexer.NextToken();
            
            foreach (Type type in types)
            {
                if (lexer.Token.GetType() == type)
                {
                    return lexer.Token;
                }
            }

            string expectedTokens = string.Join(", ", types.Select(i => i.Name).ToArray());
            throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected {expectedTokens}, got {lexer.Token.GetType().Name}");
        }

        public Program Parse()
        {
            while (!(lexer.Token is EndOfTextToken))
            {
                if (lexer.Token is FuncToken)
                {
                    program.FuncDefinitions.Add(parseFunc());
                }
                else
                {
                    program.Statements.Add(parseStatement());
                }
                lexer.NextToken();
            }

            return program;
        }

        private FuncDefinition parseFunc()
        {
            FuncDefinition funcDef = new FuncDefinition();
            
            // Accept and store identifier
            Token tmpToken = accept(typeof(IdentifierToken));
            
            // Fill properties of function definition
            funcDef.Name = (tmpToken as IdentifierToken).Value;
            funcDef.Parameters = parseParameters();
            funcDef.Body = parseBlockStatement();

            return funcDef;
        }

        private List<VarDeclarationStmt> parseParameters()
        {
            List<VarDeclarationStmt> parameters = new List<VarDeclarationStmt>();

            accept(typeof(LRoundBracketToken));

            // Expect type specifier or closing round bracket
            Token tmpToken = accept(new Type[] { typeof(TurtleToken), typeof(NumToken),
                typeof(StrToken), typeof(RRoundBracketToken) });

            while (!(tmpToken is RRoundBracketToken))
            {
                VarDeclarationStmt stmt = parseVarDeclaration();
                parameters.Add(stmt);

                tmpToken = accept(new Type[] { typeof(CommaToken), typeof(RRoundBracketToken) });

                if (tmpToken is RRoundBracketToken)
                {
                    break;
                }

                tmpToken = accept(new Type[] { typeof(TurtleToken), typeof(NumToken),
                    typeof(StrToken) });
            }

            return parameters;
        }

        private VarDeclarationStmt parseVarDeclaration()
        {
            VarDeclarationStmt varDeclarationStmt = new VarDeclarationStmt();

            // Actual type specifier
            varDeclarationStmt.Type = lexer.Token.GetType().Name;

            // Variable declaration identifier
            Token tmpToken = accept(typeof(IdentifierToken));
            varDeclarationStmt.Name = (tmpToken as IdentifierToken).Value;

            return varDeclarationStmt;
        }

        private BlockStatement parseBlockStatement()
        {
            BlockStatement blockStmt = new BlockStatement();

            accept(typeof(LSquareBracketToken));

            do
            {
                // Add more possibilities
                Token tmpToken = accept(new Type[] { typeof(RSquareBracketToken), typeof(IfToken), typeof(RepeatToken), typeof(NumToken) });

                if (tmpToken.GetType().Name == "RSquareBracketToken")
                {
                    break;
                }

                switch (tmpToken.GetType().Name)
                {
                    case "NumToken":
                        blockStmt.Statements.Add(parseVar());
                        break;



                    default:
                        throw new ParserException();
                }
            }
            while (true);

            return blockStmt;
        }

        private Statement parseVar()
        {
            VarDeclarationStmt node = new VarDeclarationStmt();

            // Set type of variable
            node.Type = lexer.Token.GetType().Name;

            accept(typeof(IdentifierToken));

            node.Name = (lexer.Token as IdentifierToken).Value;

            return node;
        }

        private Statement parseStatement()
        {
            throw new NotImplementedException();
        }

    }
}
