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
class Plus : SyntaxNode
{
    public SyntaxNode Left { get; set; }
    public SyntaxNode Right { get; set; }

    public Plus(SyntaxNode left, SyntaxNode right)
    {
        this.Left = left;
        this.Right = right;
    }
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
            throw new ArgumentException("Tokens cannot be null or empty.");
        }

        if (tokens.Count >= 2 && tokens[0].Type == TokenType.Letter
                && tokens[1].Type == TokenType.Operator && tokens[1].Lexeme == "=")
        {
            var variable = new Variable(tokens[0].Lexeme);

            var exprTokens = tokens.Skip(2).ToList();
            var expr = Parse(exprTokens);

            var assign = new Assign(variable, expr);
            return assign;
        }
        else if (tokens.Count >= 2 &&
                    tokens[1].Type == TokenType.Operator && tokens[1].Lexeme == "+")
        {
            var leftToken = tokens.Take(1).ToList();
            var rightTokens = tokens.Skip(2).ToList();

            var leftExpr = Parse(leftToken);
            var rightExpr = Parse(rightTokens);

            var plus = new Plus(leftExpr, rightExpr);
            return plus;
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
}