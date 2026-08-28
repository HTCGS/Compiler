using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public abstract class SyntaxNode
{
    public string Name { get; set; }
}

public class UnknownSyntax : SyntaxNode
{
    public UnknownSyntax(string name)
    {
        this.Name = name;
    }
}

class Function : SyntaxNode
{
    public List<SyntaxNode> Body { get; set; } = new List<SyntaxNode>();

    public Function(string name, params SyntaxNode[] body)
    {
        this.Name = name;
        this.Body.AddRange(body);
    }
}

public class Variable : SyntaxNode
{
    public Variable(string name)
    {
        this.Name = name;
    }
}

public abstract class Operation : SyntaxNode
{
    public SyntaxNode Left { get; set; }
    public SyntaxNode Right { get; set; }

    public Operation(SyntaxNode left = null, SyntaxNode right = null)
    {
        this.Left = left ?? new UnknownSyntax("Unknown");
        this.Right = right ?? new UnknownSyntax("Unknown");
    }
}

public class UnknownOperation : Operation
{
    public UnknownOperation(string errorMessage) : base(null, null)
    {
        this.Name = errorMessage;
    }
}

public class Assign : SyntaxNode
{
    public Variable Variable { get; set; }
    public SyntaxNode Expression { get; set; }

    public Assign(Variable variable, SyntaxNode expression)
    {
        this.Variable = variable;
        this.Expression = expression;
    }
}
public class Plus : Operation
{
    public Plus(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Plus() : base(null, null) { }
}

public class Multiply : Operation
{
    public Multiply(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Multiply() : base(null, null) { }
}

public class Minus : Operation
{
    public Minus(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Minus() : base(null, null) { }
}

public class Divide : Operation
{
    public Divide(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Divide() : base(null, null) { }
}

public class BracketOperation : Operation
{
    public bool IsOpen;

    public BracketOperation(bool isOpen)
    {
        this.IsOpen = isOpen;
    }
}

public class Constant : SyntaxNode
{
    public int Value { get; set; }

    public Constant(int value)
    {
        this.Value = value;
    }
}

class Return : SyntaxNode
{
    public SyntaxNode Expression { get; set; }

    public Return(SyntaxNode expression)
    {
        this.Expression = expression;
    }
}




public class TokenParser
{
    public SyntaxNode Parse(List<Token> tokens)
    {
        SyntaxNode syntaxNode = new UnknownSyntax("Unknown");

        if (tokens == null || tokens.Count == 0)
        {
            // throw new ArgumentException("Tokens cannot be null or empty.");
            return new UnknownSyntax("Tokens cannot be null or empty!");
        }

        if (tokens.Count >= 2 && tokens[0].Type == TokenType.Letter
                && tokens[1].Type == TokenType.Operator && tokens[1].Lexeme == "=")
        {
            var variable = new Variable(tokens[0].Lexeme);

            var exprTokens = tokens.Skip(2).ToList();
            if (exprTokens.Count == 0) return new UnknownSyntax("Expression can`t be empty");
            SyntaxNode exprError = CheckExpression(exprTokens);
            if (exprError != null) return exprError;

            var expr = ParseExpression(exprTokens);
            if (expr is UnknownSyntax error) return error;


            var assign = new Assign(variable, expr);
            return assign;
        }
        else if (tokens.Count >= 4 && tokens[0].Type == TokenType.Keyword
                    && tokens[0].Lexeme == "write")
        {
            if (tokens[1].Lexeme == "(" && tokens.Last().Lexeme == ")")
            {
                var exprTokens = tokens.Skip(2).SkipLast(1).ToList();
                var expr = ParseExpression(exprTokens);
                if (expr is UnknownSyntax error) return error;
                var writeLineFunc = new Function(tokens[0].Lexeme, expr);
                return writeLineFunc;
            }
            else return new UnknownSyntax("Function argument must be in bracket!");
        }
        else if (tokens.Count >= 7 && tokens[0].Type == TokenType.Keyword
                    && tokens[0].Lexeme == "if")
        {
            var thenKeywordIndex = tokens.FindIndex(t => t.Type == TokenType.Keyword && t.Lexeme == "then");
            if (thenKeywordIndex == -1) return new UnknownSyntax("then keyword is absent!");

            if (tokens[1].Lexeme != "(" && tokens[thenKeywordIndex - 1].Lexeme != ")") return new UnknownSyntax("Condition must be in bracket!");

            var conditionTokens = tokens.Skip(2).Take(thenKeywordIndex - 3).ToList();

            var conditionMiddleIndex = conditionTokens.FindIndex(t => t.Type == TokenType.Operator && t.Lexeme == "=");
            var conditionLeftTokens = conditionTokens.Take(conditionMiddleIndex).ToList();
            var conditionRightTokens = conditionTokens.Skip(conditionMiddleIndex + 1).ToList();

            var conditionLeftExpr = ParseExpression(conditionLeftTokens);
            var conditionRightExpr = ParseExpression(conditionRightTokens);

            if (conditionLeftExpr is UnknownSyntax leftError) return leftError;
            if (conditionRightExpr is UnknownSyntax rightError) return rightError;

            var thenBlockTokens = tokens.Skip(thenKeywordIndex + 1).ToList();

            var elseKeywordIndex = thenBlockTokens.FindIndex(t => t.Type == TokenType.Keyword && t.Lexeme == "else");
            if (elseKeywordIndex != -1)
            {
                var trueTokens = thenBlockTokens.Take(elseKeywordIndex).ToList();
                var falseTokens = thenBlockTokens.Skip(elseKeywordIndex + 1).ToList();

                var trueExpr = Parse(trueTokens);
                var falseExpr = Parse(falseTokens);

                if (trueExpr is UnknownSyntax) return trueExpr;
                if (falseExpr is UnknownSyntax) return falseExpr;

                var ifElseFunc = new Function(tokens[0].Lexeme, conditionLeftExpr, conditionRightExpr, trueExpr, falseExpr);
                return ifElseFunc;
            }

            var onlyTrueExpr = Parse(thenBlockTokens);
            if (onlyTrueExpr is UnknownSyntax) return onlyTrueExpr;

            var ifFunc = new Function(tokens[0].Lexeme, conditionLeftExpr, conditionRightExpr, onlyTrueExpr);
            return ifFunc;
        }
        else if (tokens.Count >= 2 && tokens[1].Type == TokenType.Operator)
        {
            var leftToken = tokens.Take(1).ToList();
            var rightTokens = tokens.Skip(2).ToList();

            var leftExpr = Parse(leftToken);
            var rightExpr = Parse(rightTokens);

            Operation operation = tokens[1].Lexeme switch  // (2+2)*2  2*(2+2) 2*((2+2)*2)
            {
                "+" => new Plus(leftExpr, rightExpr),
                "*" => new Multiply(leftExpr, rightExpr),
                _ => new UnknownOperation($"Operator '{tokens[1].Lexeme}' is not supported.")
            };

            // if ((operation is Multiply left) && (rightExpr is Plus right))
            // {
            //     operation = new Plus(new Multiply(left.Left, right.Left), right.Right);
            // }
            return operation;
        }
        else if (tokens.Count == 1 && tokens[0].Type == TokenType.Letter)
        {
            var variable = new Variable(tokens[0].Lexeme);
            return variable;
        }
        else if (tokens.Count == 1 && tokens[0].Type == TokenType.Digit)
        {
            var constant = new Constant(int.Parse(tokens[0].Lexeme));
            return constant;
        }
        return syntaxNode;
    }

    public SyntaxNode Parse(Token token)
    {
        return Parse(new List<Token> { token });
    }

    private SyntaxNode CheckExpression(List<Token> tokens)
    {
        int openBracketCount = 0;
        int closeBracketCount = 0;
        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Letter || token.Type == TokenType.Digit
                    || (token.Type == TokenType.Operator && token.Lexeme != "=")
                    || token.Type == TokenType.Bracket)
            {
                if (token.Lexeme == "(") openBracketCount++;
                if (token.Lexeme == ")") closeBracketCount++;
            }
            else return new UnknownSyntax("Unavaliable math expression token!");
        }

        if (openBracketCount != closeBracketCount) return new UnknownSyntax("Lost bracket!");

        return null;
    }

    public SyntaxNode ParseExpression(List<Token> tokens, int startToken = 0)
    {
        SyntaxNode expresion = new UnknownSyntax("Expression error!");

        var exprError = CheckExpression(tokens);
        if (exprError != null) return exprError;

        Stack<Operation> operators = new Stack<Operation>();
        Stack<SyntaxNode> operands = new Stack<SyntaxNode>();

        bool isMinus = false;
        for (int i = startToken; i < tokens.Count; i++)
        {
            var token = tokens[i];
            if (token.Type == TokenType.Digit || token.Type == TokenType.Letter)
            {
                var constOrVar = Parse(token);
                if (isMinus)
                {
                    var zeroConst = Parse(new Token(TokenType.Digit, "0"));
                    var minusExpr = operators.Pop();
                    minusExpr.Left = zeroConst;
                    minusExpr.Right = constOrVar;
                    constOrVar = minusExpr;
                    isMinus = false;
                }
                operands.Push(constOrVar);
            }
            else if (token.Type == TokenType.Operator || token.Type == TokenType.Bracket)
            {
                Operation newOperation = token.Lexeme switch
                {
                    "+" => new Plus(),
                    "*" => new Multiply(),
                    "-" => new Minus(),
                    "/" => new Divide(),
                    "(" => new BracketOperation(true),
                    ")" => new BracketOperation(false),
                    _ => new UnknownOperation($"Operator '{token.Lexeme}' is not supported.")
                };

                if (newOperation is BracketOperation openBracket && openBracket.IsOpen)
                {
                    var expr = ParseExpression(tokens, i + 1);
                    if (isMinus)
                    {
                        var zeroConst = Parse(new Token(TokenType.Digit, "0"));
                        var minusExpr = operators.Pop();
                        minusExpr.Left = zeroConst;
                        minusExpr.Right = expr;
                        expr = minusExpr;
                        isMinus = false;
                    }
                    operands.Push(expr);
                    i--;
                    continue;
                }
                if (newOperation is BracketOperation closeBracket && !closeBracket.IsOpen)
                {
                    tokens.RemoveRange(startToken - 1, i - startToken + 2);
                    break;
                }
                if (operators.Count == 0)
                {
                    if (newOperation is Minus && i == startToken) isMinus = true;
                    operators.Push(newOperation);
                }
                else
                {
                    if (operators.Count >= 1)
                    {
                        var prevOp = operators.Pop();
                        if (GetOperationWeight(prevOp) > GetOperationWeight(newOperation))
                        {
                            var prevOpRightOperand = operands.Pop();
                            var prevOpLeftOperand = operands.Pop();
                            prevOp.Right = prevOpRightOperand;
                            prevOp.Left = prevOpLeftOperand;
                            operands.Push(prevOp);
                            i--;
                            continue;
                        }
                        else
                        {
                            operators.Push(prevOp);
                            operators.Push(newOperation);
                        }

                    }
                }
            }
        }
        while (operators.Count != 0)
        {
            var op = operators.Pop();
            var rightOperand = operands.Pop();
            var leftOperands = operands.Pop();
            op.Left = leftOperands;
            op.Right = rightOperand;
            operands.Push(op);
        }
        if (operands.Count != 0) expresion = operands.Pop();
        return expresion;
    }

    private int GetOperationWeight(SyntaxNode operation)
    {
        return operation switch
        {
            Plus => 1,
            Minus => 1,
            Multiply => 2,
            Divide => 2,
            _ => 0
        };
    }
}