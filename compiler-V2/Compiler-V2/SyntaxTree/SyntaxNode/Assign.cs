public class Assign : SyntaxNode
{
    public Variable Variable { get; set; }
    public SyntaxNode Expression { get; set; }

    public Assign(Variable variable, SyntaxNode expression)
    {
        this.Name = "Assing";
        this.Variable = variable;
        this.Expression = expression;
    }
}
