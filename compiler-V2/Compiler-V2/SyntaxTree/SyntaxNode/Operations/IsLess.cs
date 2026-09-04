public class IsLess : Operation
{
    public IsLess(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "IsLess"; }

    public IsLess() : base(null, null) { this.Name = "IsLess"; }
}