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
                _ => throw new ParserException("1"),
            };
        }

        private Node parseAssignOrFuncMethCall()
        {
            accept(typeof(IdentifierToken));

            return lexer.PeekNextToken() switch
            {
                LRoundBracketToken _ => parseFuncCall(),
                DotToken _ => parseMethCall(),
                AssignmentToken _ => parseAssign(),
                _ => throw new NotImplementedException(),
            };
        }

        private FuncCall parseFuncCall()
        {
            FuncCall node = new FuncCall();

            node.Name = (lexer.Token as IdentifierToken).Value;

            accept(typeof(LRoundBracketToken));

            node.Arguments = parseArgs();

            accept(typeof(RRoundBracketToken));

            return node;
        }

        private List<Expression> parseArgs()
        {
            List<Expression> arguments = new List<Expression>();

            while (!(lexer.PeekNextToken() is RRoundBracketToken))
            {
                arguments.Add(parseExpression());

                if (lexer.PeekNextToken() is CommaToken)
                {
                    accept(typeof(CommaToken));
                    continue;
                }
            }

            return arguments;
        }

        private Node parseMethCall()
        {
            MethCall node = new MethCall();

            node.TurtleName = (lexer.Token as IdentifierToken).Value;

            accept(typeof(DotToken));
            accept(typeof(IdentifierToken));
            node.MethName = (lexer.Token as IdentifierToken).Value;

            node.Argument = parseExpression();

            accept(typeof(RRoundBracketToken));

            return node;
        }

        private AssignmentStatement parseAssign()
        {
            AssignmentStatement node = new AssignmentStatement();
            node.Variable = (lexer.Token as IdentifierToken).Value;

            accept(typeof(AssignmentToken));

            node.RightSideExpression = parseExpression();

            return node;
        }

        private Node parseIf()
        {
            IfStatement node = new IfStatement();

            accept(typeof(IfToken));
            accept(typeof(LRoundBracketToken));

            node.Condition = parseExpression();

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
            RepeatStatement node = new RepeatStatement();

            accept(typeof(RepeatToken));
            accept(typeof(LRoundBracketToken));

            node.NumOfTimes = parseExpression();

            accept(typeof(RRoundBracketToken));

            node.Body = parseBlockStatement();

            return node;
        }

        private Expression parseExpression()
        {
            Expression node = new Expression();

            node.Operands.Add(parseMultExpression());

            while (lexer.PeekNextToken() is PlusToken || lexer.PeekNextToken() is MinusToken)
            {
                node.Operators.Add(accept(new Type[] { typeof(PlusToken), typeof(MinusToken) }));
                node.Operands.Add(parseMultExpression());
            }

            return node;
        }

        private Expression parseMultExpression()
        {
            Expression node = new Expression();

            node.Operands.Add(parseParamExpression());

            while (lexer.PeekNextToken() is AsteriskToken || lexer.PeekNextToken() is SlashToken)
            {
                node.Operators.Add(accept(new Type[] { typeof(AsteriskToken), typeof(SlashToken) }));
                node.Operands.Add(parseParamExpression());
            }

            return node;
        }

        private ParamExpression parseParamExpression()
        {
            // Check if unary token
            /*if (lexer.PeekNextToken() is MinusToken)
            {
                node.Unary = true;
                accept(typeof(MinusToken));
            }*/

            // Expression
            if (lexer.PeekNextToken() is LRoundBracketToken)
            {
                ParamExpression paramExpression = new ExpressionParam();
                accept(typeof(LRoundBracketToken));
                (paramExpression as ExpressionParam).Expression = parseExpression();
                accept(typeof(RRoundBracketToken));

                return paramExpression;
            }

            // Identifier or Function Call
            if (lexer.PeekNextToken() is IdentifierToken)
            {
                Token identifier = accept(typeof(IdentifierToken));
                
                if (lexer.PeekNextToken() is LRoundBracketToken)
                {
                    ParamExpression paramExpression = new FuncCallParam();
                    (paramExpression as FuncCallParam).FuncCall = parseFuncCall();

                    return paramExpression;
                }
                else
                {
                    ParamExpression paramExpression = new IdentifierParam();
                    (paramExpression as IdentifierParam).Value = (identifier as IdentifierToken).Value;
                    return paramExpression;
                }
            }

            // Value
            if (lexer.PeekNextToken() is NumValueToken)
            {
                ParamExpression paramExpression = new NumParam();
                accept(typeof(NumValueToken));
                (paramExpression as NumParam).Value = (lexer.Token as NumValueToken).Value;
                return paramExpression;
            }

            throw new ParserException("2");

            // TODO color_val and switch it to switch maybee
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
