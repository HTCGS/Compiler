public class Compiler
{
    public Lexer Lexer;
    public TokensToASTParser TokenParser;

    public Compiler()
    {
        this.Lexer = new Lexer();
        this.TokenParser = new TokensToASTParser();
    }

    public Compiler(Lexer lexer) : this()
    {
        this.Lexer = lexer;
    }

    public Compiler(Lexer lexer, TokensToASTParser tokenParser)
    {
        this.Lexer = lexer;
        this.TokenParser = tokenParser;
    }

    public List<Token> Scan(string source)
    {
        var tokens = Lexer.Scan(source);
        return tokens;
    }

    public SyntaxNode ParseTokens(List<Token> tokens)
    {
        var program = TokenParser.Parse(tokens);
        return program;
    }


}