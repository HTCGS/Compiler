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
            // var expr = Parse(exprTokens);
            var expr = ParseExpression(exprTokens);

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

            if ((operation is Multiply left) && (rightExpr is Plus right))
            {
                operation = new Plus(new Multiply(left.Left, right.Left), right.Right);
            }
            return operation;
        }
        // else if (tokens.Count >= 2 &&
        //             tokens[1].Type == TokenType.Operator && tokens[1].Lexeme == "+")
        // {
        //     var leftToken = tokens.Take(1).ToList();
        //     var rightTokens = tokens.Skip(2).ToList();

        //     var leftExpr = Parse(leftToken);
        //     var rightExpr = Parse(rightTokens);

        //     var plus = new Plus(leftExpr, rightExpr);
        //     return plus;
        // }
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


        // Implement parsing logic here to convert tokens into an AST
        // This is a placeholder implementation and should be replaced with actual parsing logic
        // var main = new Function("main", new Assign(
        //                                         new Variable("a"),
        //                                         new Plus(
        //                                                 new Variable("b"),
        //                                                 new Constant(5))),
        //                         new Return(new Constant(0)));

        // return main;
    }

    public SyntaxNode ParseExpression(List<Token> tokens)
    {
        SyntaxNode expresion = new UnknownSyntax("Expression error!");

        Stack<SyntaxNode> operators = new Stack<SyntaxNode>();
        Stack<Token> operands = new Stack<Token>();

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Digit || token.Type == TokenType.Letter)
            {
                operands.Push(token);
            }
            else if (token.Type == TokenType.Operator)
            {
                SyntaxNode newOperation = token.Lexeme switch
                {
                    "+" => new Plus(),
                    "*" => new Multiply(),
                    _ => new UnknownSyntax($"Operator '{tokens[1].Lexeme}' is not supported.")
                };
                if (operators.Count == 0)
                {
                    operators.Push(newOperation);
                }
                else
                {
                    var topOperator = operators.Pop() as Operation;
                    if (GetOperationWeight(topOperator) > GetOperationWeight(newOperation))
                    {
                        var rightOperand = Parse(new List<Token> { operands.Pop() });
                        var leftOperand = Parse(new List<Token> { operands.Pop() });
                        topOperator.Left = leftOperand;
                        topOperator.Right = rightOperand;
                        operators.Push(topOperator);
                        operators.Push(newOperation);
                    }
                    else
                    {
                        operators.Push(topOperator);
                        operators.Push(newOperation);
                    }
                }
            }
        }

        if (operators.Count != 0)
        {
            var operation = operators.Pop() as Operation;
            var rightOperand = Parse(new List<Token> { operands.Pop() });
            var leftOperand = Parse(new List<Token> { operands.Pop() });
            operation.Right = rightOperand;
            operation.Left = leftOperand;
            expresion = operation;

            while (operators.Count != 0)
            {
                var topOperation = operators.Pop() as Operation;
                leftOperand = Parse(new List<Token> { operands.Pop() });
                topOperation.Left = leftOperand;
                topOperation.Right = expresion;
                expresion = topOperation;
            }
        }
        else
        {
            expresion = Parse(new List<Token> { operands.Pop() });
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