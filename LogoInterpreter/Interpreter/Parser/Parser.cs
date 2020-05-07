using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Program Parse()
        {
            while (true)
            {
                Token tmpToken = lexer.PeekNextToken();

                if (tmpToken is EndOfTextToken)
                {
                    return Program;
                }
                else if (tmpToken is FuncToken)
                {
                    // Create function definition
                    Program.FuncDefinitions.Add(parseFuncDef());
                }
                else
                {
                    // Parse node - instruction for interpreter
                    Program.Statements.Add(parseStatement());
                }
            }
        }

        private FuncDefinition parseFuncDef()
        {
            FuncDefinition funcDef = new FuncDefinition();

            accept(typeof(FuncToken));
            accept(typeof(IdentifierToken));
            funcDef.Name = (lexer.Token as IdentifierToken).Value;

            accept(typeof(LRoundBracketToken));

            funcDef.Parameters = parseParameters();

            accept(typeof(RRoundBracketToken));
            
            funcDef.Body = parseBlockStatement();

            return funcDef;
        }

        private List<VarDeclaration> parseParameters()
        {
            List<VarDeclaration> node = new List<VarDeclaration>();
            
            while (!(lexer.PeekNextToken() is RRoundBracketToken))
            {
                node.Add(parseVarDeclaration());

                if (lexer.PeekNextToken() is CommaToken)
                {
                    accept(typeof(CommaToken));
                    continue;
                }
            }

            return node;
        }

        private VarDeclaration parseVarDeclaration()
        {
            VarDeclaration node = new VarDeclaration();

            accept(new Type[] { typeof(TurtleToken), typeof(NumToken), typeof(StrToken) });
            node.Type = lexer.Token.GetType().Name;

            accept(typeof(IdentifierToken));
            node.Name = (lexer.Token as IdentifierToken).Value;

            return node;
        }

        private BlockStatement parseBlockStatement()
        {
            BlockStatement blockStmt = new BlockStatement();

            accept(typeof(LSquareBracketToken));

            while (!(lexer.PeekNextToken() is RSquareBracketToken))
            {
                blockStmt.Statements.Add(parseStatement());
            }
            
            accept(typeof(RSquareBracketToken));

            return blockStmt;
        }

        private Node parseStatement()
        {
            // Create appropriate statement based on first token
            return lexer.PeekNextToken() switch
            {
                NumToken _ => parseVarDeclaration(),
                StrToken _ => parseVarDeclaration(),
                TurtleToken _ => parseVarDeclaration(),

                IfToken _ => parseIf(),
                RepeatToken _ => parseRepeat(),

                IdentifierToken _ => parseAssignOrFuncMethCall(),
                _ => throw new ParserException(),
            };
        }

        private Node parseAssignOrFuncMethCall()
        {
            accept(typeof(IdentifierToken));

            return lexer.PeekNextToken() switch
            {
                LRoundBracketToken _ => parseFuncCall(),
                DotToken _ => parseMethCall(),
                EqualToken _ => parseAssign(),
                _ => throw new NotImplementedException(),
            };
        }

        private Node parseFuncCall()
        {
            FuncCall node = new FuncCall();

            node.Name = (lexer.Token as IdentifierToken).Value;

            accept(typeof(LRoundBracketToken));

            node.Arguments = parseArgs();

            accept(typeof(RRoundBracketToken));

            return node;
        }

        private List<string> parseArgs()
        {
            List<string> arguments = new List<string>();

            while (!(lexer.PeekNextToken() is RRoundBracketToken))
            {
                arguments.Add(parseArg());

                if (lexer.PeekNextToken() is CommaToken)
                {
                    accept(typeof(CommaToken));
                    continue;
                }
            }

            return arguments;
        }

        private string parseArg()
        {
            accept(typeof(IdentifierToken));

            return (lexer.Token as IdentifierToken).Value;
        }

        private Node parseMethCall()
        {
            MethCall node = new MethCall();

            node.Turtle = (lexer.Token as IdentifierToken).Value;

            accept(typeof(DotToken));
            accept(typeof(IdentifierToken));
            node.Name = (lexer.Token as IdentifierToken).Value;

            node.Argument = parseArg();

            accept(typeof(RRoundBracketToken));

            return node;
        }

        private Node parseAssign()
        {
            accept(typeof(EqualToken));

            // TODO accept expression
            
            throw new NotImplementedException();
        }

        private Node parseIf()
        {
            IfStatement node = new IfStatement();

            accept(typeof(IfToken));
            accept(typeof(LRoundBracketToken));

            // TODO accept expression
            node.Predicate = parseExpression();

            accept(typeof(RRoundBracketToken));

            node.Body = parseBlockStatement();

            if (lexer.PeekNextToken() is ElseToken)
            {
                node.ElseBody = parseBlockStatement();
            }

            return node;
        }

        private Node parseRepeat()
        {
            throw new NotImplementedException();
        }


        private Node parseExpression()
        {
            throw new NotImplementedException();
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
    }
}
