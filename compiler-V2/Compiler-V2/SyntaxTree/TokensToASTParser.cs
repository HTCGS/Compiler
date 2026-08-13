using System.Linq.Expressions;
using System.Runtime.CompilerServices;

public abstract class SyntaxNode
{
    public string Name { get; set; }
}

class UnknownSyntax : SyntaxNode
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

class Variable : SyntaxNode
{
    public Variable(string name)
    {
        this.Name = name;
    }
}

abstract class Operation : SyntaxNode
{
    public SyntaxNode Left { get; set; }
    public SyntaxNode Right { get; set; }

    public Operation(SyntaxNode left, SyntaxNode right = null)
    {
        this.Left = left;
        this.Right = right ?? new UnknownSyntax("Unknown");
    }
}

class Assign : SyntaxNode
{
    public Variable Variable { get; set; }
    public SyntaxNode Expression { get; set; }

    public Assign(Variable variable, SyntaxNode expression)
    {
        this.Variable = variable;
        this.Expression = expression;
    }
}
class Plus : Operation
{
    public Plus(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Plus() : base(new UnknownSyntax("Unknown"), new UnknownSyntax("Unknown")) { }
}

class Multiply : Operation
{
    public Multiply(SyntaxNode left, SyntaxNode right) : base(left, right) { }

    public Multiply() : base(new UnknownSyntax("Unknown"), new UnknownSyntax("Unknown")) { }
}

class Constant : SyntaxNode
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




public class TokensToASTParser
{
    public SyntaxNode Parse(List<Token> tokens)
    {
        SyntaxNode syntaxNode = new UnknownSyntax("Unknown");

        if (tokens == null || tokens.Count == 0)
        {
            // throw new ArgumentException("Tokens cannot be null or empty.");
            return syntaxNode;
        }

        if (tokens.Count >= 2 && tokens[0].Type == TokenType.Letter
                && tokens[1].Type == TokenType.Operator && tokens[1].Lexeme == "=")
        {
            var variable = new Variable(tokens[0].Lexeme);

            var exprTokens = tokens.Skip(2).ToList();
            if (exprTokens.Count == 0) return new UnknownSyntax("Expression can`t be empty");
            // var expr = Parse(exprTokens);   ((2+1)
            SyntaxNode exprError = CheckExpression(exprTokens);
            if (exprError != null) return exprError;
            var expr = ParseExpression(exprTokens);
            if (expr is UnknownSyntax error) return error;


            var assign = new Assign(variable, expr);
            return assign;
        }
        else if (tokens.Count >= 2 && tokens[1].Type == TokenType.Operator)
        {
            var leftToken = tokens.Take(1).ToList();
            var rightTokens = tokens.Skip(2).ToList();

            var leftExpr = Parse(leftToken);
            var rightExpr = Parse(rightTokens);

            SyntaxNode operation = tokens[1].Lexeme switch  // (2+2)*2  2*(2+2) 2*((2+2)*2)
            {
                "+" => new Plus(leftExpr, rightExpr),
                "*" => new Multiply(leftExpr, rightExpr),
                _ => new UnknownSyntax($"Operator '{tokens[1].Lexeme}' is not supported.")
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

    private SyntaxNode CheckExpression(List<Token> tokens)
    {
        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Letter || token.Type == TokenType.Digit
                    || (token.Type == TokenType.Operator && token.Lexeme != "=")) continue;
            else return new UnknownSyntax("Unavaliable math expression token!");
        }
        return null;
    }

    public SyntaxNode ParseExpression(List<Token> tokens)
    {
        SyntaxNode expresion = new UnknownSyntax("Expression error!");

        // Stack<SyntaxNode> operators = new Stack<SyntaxNode>();
        // Stack<Token> operands = new Stack<Token>();
        List<SyntaxNode> operators = new List<SyntaxNode>();
        List<Token> operands = new List<Token>();

        // foreach (var token in tokens)
        for (int i = 0; i <= tokens.Count - 1; i++)
        {
            var token = tokens[i];
            if (token.Type == TokenType.Digit || token.Type == TokenType.Letter)
            {
                operands.Add(token);
            }
            else if (token.Type == TokenType.Operator)
            {
                SyntaxNode newOperation = token.Lexeme switch
                {
                    "+" => new Plus(),
                    "*" => new Multiply(),
                    _ => new UnknownSyntax($"Operator '{token.Lexeme}' is not supported.")
                };
                if (operators.Count == 0)
                {
                    operators.Add(newOperation);
                }
                else
                {
                    var topOperator = operators.Last() as Operation;
                    if (GetOperationWeight(topOperator) > GetOperationWeight(newOperation))
                    {
                        var topOpRightOperand = Parse(new List<Token> { operands.Last() });
                        var topOpLeftOperand = Parse(new List<Token> { operands.SkipLast(1).Last() });
                        topOperator.Left = topOpLeftOperand;
                        topOperator.Right = topOpRightOperand;

                        operands = operands.SkipLast(2).ToList();
                        operators.Add(newOperation);
                    }
                    else
                    {
                        operators.Add(newOperation);
                    }
                }
            }
        }

        if (operators.Count != 0)
        {
            Operation lastOp = operators.First() as Operation;
            if (lastOp.Left is UnknownSyntax)
            {
                if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                var leftOperand = Parse(operands.Take(1).ToList());
                operands = operands.Skip(1).ToList();
                lastOp.Left = leftOperand;
            }
            if (lastOp.Right is UnknownSyntax)
            {
                if (operators.Count >= 2)
                {
                    var nextOp = operators.Skip(1).Take(1).First() as Operation;
                    if (nextOp.Left is UnknownSyntax)
                    {
                        if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                        var nextOpLeftOperand = Parse(operands.Take(1).ToList());
                        nextOp.Left = nextOpLeftOperand;
                        operands = operands.Skip(1).ToList();
                    }
                    if (nextOp.Right is UnknownSyntax)
                    {
                        if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                        var nextOpRightOperand = Parse(operands.Take(1).ToList());
                        nextOp.Right = nextOpRightOperand;
                        operands = operands.Skip(1).ToList();
                    }
                    lastOp.Right = nextOp;
                }
                else
                {
                    if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                    var rightOperand = Parse(operands.Take(1).ToList());
                    operands = operands.Skip(1).ToList();
                    lastOp.Right = rightOperand;
                }
            }
            expresion = lastOp;
            for (int i = 1; i < operators.Count; i++)
            {
                Operation operation = operators[i] as Operation;
                if (operation.Left is not UnknownSyntax && operation.Right is not UnknownSyntax) continue;
                if (operation.Left is UnknownSyntax)
                {
                    operation.Left = lastOp;
                }
                if (operation.Right is UnknownSyntax)
                {
                    if (i < operators.Count - 1)
                    {
                        var nextOp = operators[i + 1] as Operation;
                        if (nextOp.Left is UnknownSyntax)
                        {
                            if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                            var nextOpLeftOperand = Parse(operands.Take(1).ToList());
                            nextOp.Left = nextOpLeftOperand;
                            operands = operands.Skip(1).ToList();
                        }
                        if (nextOp.Right is UnknownSyntax)
                        {
                            if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                            var nextOpRightOperand = Parse(operands.Take(1).ToList());
                            nextOp.Right = nextOpRightOperand;
                            operands = operands.Skip(1).ToList();
                        }
                        operation.Right = nextOp;
                    }
                    else
                    {
                        if (operands.Count == 0) return new UnknownSyntax("Operand is absent!");
                        var rightOperand = Parse(operands.Take(1).ToList());
                        operands = operands.Skip(1).ToList();
                        operation.Right = rightOperand;
                    }
                }
                expresion = operation;
            }
        }
        else
        {
            expresion = Parse(new List<Token> { operands.First() });
        }
        return expresion;
    }

    private int GetOperationWeight(SyntaxNode operation)
    {
        return operation switch
        {
            Plus => 1,
            Multiply => 2,
            _ => 0
        };
    }
}