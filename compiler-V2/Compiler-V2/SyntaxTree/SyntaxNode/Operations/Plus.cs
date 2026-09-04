public class Plus : Operation
{
    public Plus(SyntaxNode left, SyntaxNode right) : base(left, right)
    {
        this.Name = "Plus";
    }

    public Plus() : base(null, null) { this.Name = "Plus"; }
}
