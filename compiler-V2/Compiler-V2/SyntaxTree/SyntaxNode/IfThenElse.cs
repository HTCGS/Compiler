public class IfThenElse : SyntaxNode
{
    public SyntaxNode Condition;
    public SyntaxNode TrueExpression;
    public SyntaxNode FalseExpression;

    public IfThenElse(SyntaxNode condition, SyntaxNode trueExpression)
    {
        this.Name = "IfThen";
        this.Condition = condition;
        this.TrueExpression = trueExpression;
    }

    public IfThenElse(SyntaxNode condition, SyntaxNode trueExpression, SyntaxNode falseExpression)
    {
        this.Name = "IfThenElse";
        this.Condition = condition;
        this.TrueExpression = trueExpression;
        this.FalseExpression = falseExpression;
    }
}