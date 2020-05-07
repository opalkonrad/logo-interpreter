using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace LogoInterpreter.Interpreter
{
    public class Parser
    {
        private readonly Lexer lexer;
        public Program Program { get; private set; } = new Program();

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
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
                lexer.NextToken();

                if (lexer.Token is FuncToken)
                {
                    Program.FuncDefinitions.Add(parseFunc());
                }
                else
                {
                    Program.Statements.Add(parseStatement());
                }
            }

            return Program;
        }

        private FuncDefinition parseFunc()
        {
            FuncDefinition funcDef = new FuncDefinition();

            // Accept identifier
            accept(typeof(IdentifierToken));

            // Fill properties of function definition
            funcDef.Name = (lexer.Token as IdentifierToken).Value;

            accept(typeof(LRoundBracketToken));
            accept(new Type[] { typeof(TurtleToken), typeof(NumToken), typeof(StrToken) });

            funcDef.Parameters = parseParameters();

            accept(typeof(LSquareBracketToken));

            funcDef.Body = parseBlockStatement();

            return funcDef;
        }

        private List<VarDeclarationStmt> parseParameters()
        {
            List<VarDeclarationStmt> parameters = new List<VarDeclarationStmt>();
            VarDeclarationStmt parameter;

            try
            {
                while (!(lexer.Token is RRoundBracketToken))
                {
                    parameter = parseVarDeclaration();

                    parameters.Add(parameter);

                    accept(new Type[] { typeof(CommaToken), typeof(RRoundBracketToken) });
                }
            }
            catch (ParserException)
            {
                // Rethrow exception if it wasn't closing bracket (thrown by parseVarDeclaration()) which means no parameters
                if (!(lexer.Token is RRoundBracketToken))
                {
                    throw;
                }
            }

            return parameters;
        }

        private VarDeclarationStmt parseVarDeclaration()
        {
            VarDeclarationStmt varDeclarationStmt = new VarDeclarationStmt();

            // Actual type specifier
            varDeclarationStmt.Type = lexer.Token.GetType().Name;

            // Variable identifier
            accept(typeof(IdentifierToken));
            varDeclarationStmt.Name = (lexer.Token as IdentifierToken).Value;

            return varDeclarationStmt;
        }

        private BlockStatement parseBlockStatement()
        {
            BlockStatement blockStmt = new BlockStatement();

            

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

        private Statement parseStatement()
        {
            throw new NotImplementedException();
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

        

    }
}
