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
