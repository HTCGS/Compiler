abstract class AbstractSyntaxElement
{
    public string Name { get; set; }
    public List<AbstractSyntaxElement> Children { get; set; } = new List<AbstractSyntaxElement>();
}

class Function : AbstractSyntaxElement
{
    public Function(string name, params AbstractSyntaxElement[] children)
    {
        this.Name = name;
        this.Children.AddRange(children);
    }
}

class Variable : AbstractSyntaxElement
{
    public Variable(string name)
    {
        this.Name = name;
    }
}

class Assign : AbstractSyntaxElement
{
    public Variable Variable { get; set; }
    public AbstractSyntaxElement Expression { get; set; }

    public Assign(Variable variable, AbstractSyntaxElement expression)
    {
        this.Variable = variable;
        this.Expression = expression;
    }
}
class Plus : AbstractSyntaxElement
{
    public AbstractSyntaxElement Left { get; set; }
    public AbstractSyntaxElement Right { get; set; }

    public Plus(AbstractSyntaxElement left, AbstractSyntaxElement right)
    {
        this.Left = left;
        this.Right = right;
    }
}

class Constant : AbstractSyntaxElement
{
    public int Value { get; set; }

    public Constant(int value)
    {
        this.Value = value;
    }
}

class Return : AbstractSyntaxElement
{
    public AbstractSyntaxElement Expression { get; set; }

    public Return(AbstractSyntaxElement expression)
    {
        this.Expression = expression;
    }
}




class ASTParser
{
    public AbstractSyntaxElement Parse(List<Token> tokens)
    {
        // Implement parsing logic here to convert tokens into an AST
        // This is a placeholder implementation and should be replaced with actual parsing logic
        var main = new Function("main", new Assign(
                                                new Variable("a"),
                                                new Plus(
                                                        new Variable("b"),
                                                        new Constant(5))),
                                new Return(new Constant(0)));

        return main;
    }
}