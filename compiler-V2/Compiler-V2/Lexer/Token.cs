public class Token
{
    public TokenType Type { get; set; }
    public string Lexeme { get; set; }

    public Token(TokenType type, string value)
    {
        this.Type = type;
        this.Lexeme = value;
    }
}