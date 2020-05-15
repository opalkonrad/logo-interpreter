using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
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
            lexer.NextToken();
        }

        public Program Parse()
        {
            Dictionary<string, FuncDefinition> funcDefs = new Dictionary<string, FuncDefinition>();
            FuncDefinition funcDef;

            List<INode> statements = new List<INode>();
            INode statement;

            while((funcDef = parseFuncDef()) != null | (statement = parseStatement()) != null)
            {
                if (funcDef != null)
                {
                    funcDefs.Add(funcDef.Name, funcDef);
                }
                else
                {
                    statements.Add(statement);
                }
            }
            
            return new Program(statements, funcDefs);
        }

        private FuncDefinition parseFuncDef()
        {
            if (lexer.Token is FuncToken)
            {
                lexer.NextToken();

                Token tmpToken = accept(typeof(IdentifierToken));
                string funcName = (tmpToken as IdentifierToken).Value;
                
                accept(typeof(LRoundBracketToken));

                List<VarDeclaration> parameters = parseParameters();

                accept(typeof(RRoundBracketToken));

                BlockStatement body = parseBlockStatement();

                return new FuncDefinition(funcName, parameters, body);
            }
            else
            {
                return null;
            }
        }

        private List<VarDeclaration> parseParameters()
        {
            List<VarDeclaration> parameters = new List<VarDeclaration>();
            VarDeclaration varDecl;

            if ((varDecl = parseVarDeclaration()) != null)
            {
                parameters.Add(varDecl);

                while (lexer.Token is CommaToken)
                {
                    if ((varDecl = parseVarDeclaration()) != null)
                    {
                        parameters.Add(varDecl);
                    }
                    else
                    {
                        throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected TurtleToken, NumToken, StrToken, got {lexer.Token.GetType().Name}");
                    }
                }
            }

            return parameters;
        }

        private VarDeclaration parseVarDeclaration()
        {
            if (lexer.Token is TurtleToken || lexer.Token is NumToken || lexer.Token is StrToken)
            {
                string type = lexer.Token.GetType().Name;

                lexer.NextToken();

                Token tmpToken = accept(typeof(IdentifierToken));
                string name = (tmpToken as IdentifierToken).Value;

                return new VarDeclaration(type, name);
            }
            else
            {
                return null;
            }
        }

        private BlockStatement parseBlockStatement()
        {
            BlockStatement blockStmt = new BlockStatement();
            INode stmt;

            accept(typeof(LSquareBracketToken));

            while ((stmt = parseStatement()) != null)
            {
                blockStmt.Statements.Add(stmt);
            }

            accept(typeof(RSquareBracketToken));

            return blockStmt;
        }

        private INode parseStatement()
        {
            return parseVarDeclaration() ?? parseIf() ?? parseRepeat() ?? parseReturn() ?? parseAssignOrFuncMethCall() ?? null;
        }

        private ReturnStatement parseReturn()
        {
            if (lexer.Token is ReturnToken)
            {
                lexer.NextToken();

                AddExpression expression = parseExpression();

                return new ReturnStatement(expression);
            }
            else
            {
                return null;
            }
        }

        private INode parseAssignOrFuncMethCall()
        {
            if (lexer.Token is IdentifierToken)
            {
                string identifier = (lexer.Token as IdentifierToken).Value;

                lexer.NextToken();

                return parseFuncCall(identifier) ?? parseMethCall(identifier) ?? parseAssign(identifier) ?? null;
            }
            else
            {
                return null;
            }
        }

        private FuncCall parseFuncCall(string identifier)
        {
            if (lexer.Token is LRoundBracketToken)
            {
                lexer.NextToken();

                List<AddExpression> arguments = parseArgs();

                accept(typeof(RRoundBracketToken));

                return new FuncCall(identifier, arguments);
            }
            else
            {
                return null;
            }
        }

        private List<AddExpression> parseArgs()
        {
            List<AddExpression> arguments = new List<AddExpression>();
            AddExpression expr;

            if ((expr = parseExpression()) != null)
            {
                arguments.Add(expr);

                while (lexer.Token is CommaToken)
                {
                    if ((expr = parseExpression()) != null)
                    {
                        arguments.Add(expr);
                    }
                    else
                    {
                        throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected expression, got {lexer.Token.GetType().Name}");
                    }
                }
            }

            return arguments;
        }

        private INode parseMethCall(string identifier)
        {
            if (lexer.Token is DotToken)
            {
                lexer.NextToken();

                Token methNameToken = accept(typeof(IdentifierToken));
                string methName = (methNameToken as IdentifierToken).Value;

                AddExpression argument = parseExpression();

                accept(typeof(RRoundBracketToken));

                return new MethCall(identifier, methName, argument);
            }
            else
            {
                return null;
            }
        }

        private AssignmentStatement parseAssign(string identifier)
        {
            if (lexer.Token is AssignmentToken)
            {
                lexer.NextToken();

                AddExpression rightSide = parseExpression();

                return new AssignmentStatement(identifier, rightSide);
            }
            else
            {
                return null;
            }
        }

        private INode parseIf()
        {
            if (lexer.Token is IfToken)
            {
                lexer.NextToken();

                accept(typeof(LRoundBracketToken));

                AddExpression condition = parseExpression();

                accept(typeof(RRoundBracketToken));

                BlockStatement body = parseBlockStatement();

                if (lexer.Token is ElseToken)
                {
                    lexer.NextToken();

                    BlockStatement elseBody = parseBlockStatement();

                    return new IfStatement(condition, body, elseBody);
                }

                return new IfStatement(condition, body, new BlockStatement());
            }
            else
            {
                return null;
            }
        }

        private RepeatStatement parseRepeat()
        {
            if (lexer.Token is RepeatToken)
            {
                lexer.NextToken();

                accept(typeof(LRoundBracketToken));

                AddExpression numOfTimes = parseExpression();

                accept(typeof(RRoundBracketToken));

                BlockStatement body = parseBlockStatement();

                return new RepeatStatement(numOfTimes, body);
            }
            else
            {
                return null;
            }
        }

        private AddExpression parseExpression()
        {
            AddExpression node = new AddExpression();

            node.Operands.Add(parseMultExpression());

            while (lexer.Token is PlusToken || lexer.Token is MinusToken)
            {
                switch (lexer.Token)
                {
                    case PlusToken _:
                        node.Operators.Add(PlusToken.Text);
                        break;

                    case MinusToken _:
                        node.Operators.Add(MinusToken.Text);
                        break;
                }
                lexer.NextToken();
                
                node.Operands.Add(parseMultExpression());
            }

            return node;
        }

        private MultExpression parseMultExpression()
        {
            MultExpression node = new MultExpression();

            node.Operands.Add(parseParamExpression());

            while (lexer.Token is AsteriskToken || lexer.Token is SlashToken)
            {
                switch (lexer.Token)
                {
                    case AsteriskToken _:
                        node.Operators.Add(AsteriskToken.Text);
                        break;

                    case SlashToken _:
                        node.Operators.Add(SlashToken.Text);
                        break;
                }
                lexer.NextToken();

                node.Operands.Add(parseParamExpression());
            }

            return node;
        }

        private INode parseParamExpression()
        {
            bool unary = false;

            // Check if unary token
            if (lexer.Token is MinusToken)
            {
                unary = true;
                lexer.NextToken();
            }

            // Expression
            if (lexer.Token is LRoundBracketToken)
            {
                lexer.NextToken();
                AddExpression expr = parseExpression();
                accept(typeof(RRoundBracketToken));

                return new ExpressionExprParam(unary, expr);
            }

            // Identifier or function call
            if (lexer.Token is IdentifierToken)
            {
                string identifier = (lexer.Token as IdentifierToken).Value;
                lexer.NextToken();

                // Try to build function call
                FuncCall funcCall = parseFuncCall(identifier);

                if (funcCall != null)
                {
                    return new FuncCallExprParam(unary, funcCall);
                }
                else
                {
                    return new IdentifierExprParam(unary, identifier);
                }
            }

            // Value
            if (lexer.Token is NumValueToken)
            {
                double value = (lexer.Token as NumValueToken).Value;
                lexer.NextToken();
                return new NumValueExprParam(unary, value);
            }

            return null;
        }

        private Token accept(Type type)
        {
            // Return expected token
            if (lexer.Token.GetType() == type)
            {
                Token tmpToken = lexer.Token;
                lexer.NextToken();
                return tmpToken;
            }
            else
            {
                throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected {type.Name}, got {lexer.Token.GetType().Name}");
            }            
        }

        private Token accept(Type[] types)
        {
            // Return expected token
            foreach (Type type in types)
            {
                if (lexer.Token.GetType() == type)
                {
                    Token tmpToken = lexer.Token;
                    lexer.NextToken();
                    return tmpToken;
                }
            }

            string expectedTokens = string.Join(", ", types.Select(i => i.Name).ToArray());
            throw new ParserException($"Wrong token in {lexer.Token.Position}. Expected {expectedTokens}, got {lexer.Token.GetType().Name}");
        }
    }
}
