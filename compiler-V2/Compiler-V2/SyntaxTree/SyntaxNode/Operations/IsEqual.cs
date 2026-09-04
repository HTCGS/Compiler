public class IsEqual : Operation
{
    public IsEqual(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "IsEqual"; }

    public IsEqual() : base(null, null) { this.Name = "IsEqual"; }
}