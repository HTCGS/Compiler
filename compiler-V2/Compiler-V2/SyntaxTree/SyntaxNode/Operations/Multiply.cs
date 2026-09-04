public class Multiply : Operation
{
    public Multiply(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "Multiply"; }

    public Multiply() : base(null, null) { this.Name = "Multiply"; }
}
