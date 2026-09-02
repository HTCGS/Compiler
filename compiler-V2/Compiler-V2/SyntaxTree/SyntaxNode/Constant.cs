public class Constant : SyntaxNode
{
    public int Value { get; set; }

    public Constant(int value)
    {
        this.Value = value;
    }
}
