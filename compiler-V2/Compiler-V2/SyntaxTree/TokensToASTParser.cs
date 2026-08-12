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
                    _ => new UnknownSyntax($"Operator '{tokens[1].Lexeme}' is not supported.")
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

                        // operators.Push(topOperator);
                        operators.Add(newOperation);

                        // var nextToken = tokens.Skip(i + 1).Take(1).ToList();
                        // var nextTokenSyntax = Parse(nextToken);
                        // if (nextTokenSyntax is Variable || nextTokenSyntax is Constant)
                        // {
                        //     (newOperation as Operation).Right = nextTokenSyntax;
                        //     (newOperation as Operation).Left = topOperator;
                        //     operators.Push(newOperation);
                        //     i += 2;
                        // }


                        // var rightOperand = Parse(new List<Token> { operands.Pop() });
                        // var newOpLeftOperand = Parse(new List<Token> { operands.Pop() });
                        // (newOperation as Operation).Left = newOpLeftOperand;

                        // var topOpLeftOperand = Parse(new List<Token> { operands.Pop() });
                        // topOperator.Left = topOpLeftOperand;
                        // topOperator.Right = newOperation;
                        // operators.Push(topOperator);

                        // var nextToken = tokens.Skip(i + 1).Take(1).ToList();
                        // var nextTokenSyntax = Parse(nextToken);
                        // if (nextTokenSyntax is Variable || nextTokenSyntax is Constant)
                        // {
                        //     (newOperation as Operation).Right = nextTokenSyntax;
                        //     i += 2;
                        // }
                    }
                    else
                    {
                        // operators.Push(topOperator);
                        operators.Add(newOperation);
                    }
                }
            }
        }

        if (operators.Count != 0)
        {
            // var operation = operators.Pop() as Operation;
            // if (operation.Right is UnknownSyntax)
            // {
            //     var rightOperand = Parse(new List<Token> { operands.Pop() });
            //     operation.Right = rightOperand;
            // }
            // if (operation.Left is UnknownSyntax)
            // {
            //     var leftOperand = Parse(new List<Token> { operands.Pop() });
            //     operation.Left = leftOperand;
            // }
            // expresion = operation;

            // while (operators.Count != 0)
            // {
            //     var topOperation = operators.Pop() as Operation;
            //     topOperation.Right = expresion;


            //     leftOperand = Parse(new List<Token> { operands.Pop() });
            //     topOperation.Left = leftOperand;
            //     expresion = topOperation;
            // }

            Operation lastOp = operators.First() as Operation;
            if (lastOp.Left is UnknownSyntax)
            {
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
                        var nextOpLeftOperand = Parse(operands.Take(1).ToList());
                        nextOp.Left = nextOpLeftOperand;
                        operands = operands.Skip(1).ToList();
                    }
                    if (nextOp.Right is UnknownSyntax)
                    {
                        var nextOpRightOperand = Parse(operands.Take(1).ToList());
                        nextOp.Right = nextOpRightOperand;
                        operands = operands.Skip(1).ToList();
                    }
                    lastOp.Right = nextOp;
                }
                else
                {
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
                    // var leftOperand = Parse(operands.Take(1).ToList());
                    // operands = operands.Skip(1).ToList();
                    operation.Left = lastOp;
                }
                if (operation.Right is UnknownSyntax)
                {
                    if (i < operators.Count - 1)
                    {
                        var nextOp = operators[i + 1] as Operation;
                        if (nextOp.Left is UnknownSyntax)
                        {
                            var nextOpLeftOperand = Parse(operands.Take(1).ToList());
                            nextOp.Left = nextOpLeftOperand;
                            operands = operands.Skip(1).ToList();
                        }
                        if (nextOp.Right is UnknownSyntax)
                        {
                            var nextOpRightOperand = Parse(operands.Take(1).ToList());
                            nextOp.Right = nextOpRightOperand;
                            operands = operands.Skip(1).ToList();
                        }
                        operation.Right = nextOp;
                    }
                    else
                    {
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