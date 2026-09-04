public class Minus : Operation
{
    public Minus(SyntaxNode left, SyntaxNode right) : base(left, right) { this.Name = "Minus"; }

    public Minus() : base(null, null) { this.Name = "Minus"; }
}
