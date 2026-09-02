class Function : SyntaxNode
{
    public List<SyntaxNode> Body { get; set; } = new List<SyntaxNode>();

    public Function(string name, params SyntaxNode[] body)
    {
        this.Name = name;
        this.Body.AddRange(body);
    }
}
