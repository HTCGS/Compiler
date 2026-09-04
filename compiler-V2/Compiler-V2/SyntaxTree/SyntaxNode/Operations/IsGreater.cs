public class IsGreater : Operation
{
    public IsGreater(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "IsGreater"; }

    public IsGreater() : base(null, null) { this.Name = "IsGreater"; }
}
