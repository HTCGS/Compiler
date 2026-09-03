public class Divide : Operation
{
    public Divide(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "Divide"; }

    public Divide() : base(null, null) { this.Name = "Divide"; }
}
