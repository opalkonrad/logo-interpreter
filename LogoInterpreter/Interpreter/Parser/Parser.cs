using System;
using System.Collections.Generic;

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
            INode statement = null;

            while((funcDef = parseFuncDef()) != null || (statement = parseStatement()) != null)
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

            if (!(lexer.Token is EndOfTextToken))
            {
                throw new ParserException("Did not reach end of text, neither statement nor function definition found in " + lexer.Token.Position);
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
                    lexer.NextToken();

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

                AddExpression expression = parseExpression()
                    ?? throw new ParserException("Expected expression in " + lexer.Token.Position);

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
                    lexer.NextToken();

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

                accept(typeof(LRoundBracketToken));

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

                AddExpression rightSide = parseExpression()
                     ?? throw new ParserException("Expected expression in " + lexer.Token.Position);

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

                EqualCondition condition = parseCondition()
                    ?? throw new ParserException("Expected condition in " + lexer.Token.Position);

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

                AddExpression numOfTimes = parseExpression()
                     ?? throw new ParserException("Expected expression in " + lexer.Token.Position);

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
            MultExpression firstOperand = parseMultExpression();

            if (firstOperand != null)
            {
                AddExpression node = new AddExpression();
                node.Operands.Add(firstOperand);

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
            else
            {
                return null;
            }
        }

        private MultExpression parseMultExpression()
        {
            INode firstOperand = parseParamExpression();

            if (firstOperand != null)
            {
                MultExpression node = new MultExpression();
                node.Operands.Add(firstOperand);

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
            else
            {
                return null;
            }
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
            if (lexer.Token is LRoundBracketToken && !unary)
            {
                lexer.NextToken();
                AddExpression expr = parseExpression()
                     ?? throw new ParserException("Expected expression in " + lexer.Token.Position);
                accept(typeof(RRoundBracketToken));

                return new ExpressionExprParam(expr);
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

            // Num value
            if (lexer.Token is NumValueToken)
            {
                double value = (lexer.Token as NumValueToken).Value;
                lexer.NextToken();
                return new NumValueExprParam(unary, value);
            }

            // Str value
            if (lexer.Token is StrValueToken && !unary)
            {
                string value = (lexer.Token as StrValueToken).Value;
                lexer.NextToken();
                return new StrValueExprParam(value);
            }

            return null;
        }

        private EqualCondition parseCondition()
        {
            RelationalCondition firstOperand = parseRelationalCondition();

            if (firstOperand != null)
            {
                EqualCondition equalCondition = new EqualCondition();
                equalCondition.Operands.Add(firstOperand);

                while (lexer.Token is EqualToken || lexer.Token is NotEqualToken)
                {
                    switch (lexer.Token)
                    {
                        case EqualToken _:
                            firstOperand.Operators.Add(EqualToken.Text);
                            break;

                        case NotEqualToken _:
                            firstOperand.Operators.Add(NotEqualToken.Text);
                            break;
                    }
                    lexer.NextToken();

                    equalCondition.Operands.Add(parseRelationalCondition());
                }

                return equalCondition;
            }
            else
            {
                return null;
            }
        }

        private RelationalCondition parseRelationalCondition()
        {
            AddExpression firstOperand = parseExpression();

            if (firstOperand != null)
            {
                RelationalCondition relCond = new RelationalCondition();
                relCond.Operands.Add(firstOperand);

                while (lexer.Token is LessThanToken || lexer.Token is LessEqualThanToken
                    || lexer.Token is GreaterThanToken || lexer.Token is GreaterEqualThanToken)
                {
                    switch (lexer.Token)
                    {
                        case LessThanToken _:
                            relCond.Operators.Add(LessThanToken.Text);
                            break;

                        case LessEqualThanToken _:
                            relCond.Operators.Add(LessEqualThanToken.Text);
                            break;

                        case GreaterThanToken _:
                            relCond.Operators.Add(GreaterThanToken.Text);
                            break;

                        case GreaterEqualThanToken _:
                            relCond.Operators.Add(GreaterEqualThanToken.Text);
                            break;
                    }
                    lexer.NextToken();

                    relCond.Operands.Add(parseExpression());
                }

                return relCond;
            }
            else
            {
                return null;
            }
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
    }
}
