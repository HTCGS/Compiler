public class Lexer
{
    public string Source { get; set; }

    public List<Token> Tokens { get; set; }

    public Lexer()
    {
        this.Source = string.Empty;
        this.Tokens = new List<Token>();
    }

    public Lexer(string source) : this()
    {
        this.Source = source;
    }

    public List<Token> Scan()
    {
        this.Tokens = new List<Token>();

        foreach (char c in this.Source)
        {
            if (char.IsLetter(c))
            {
                this.Tokens.Add(new Token(TokenType.Letter, c.ToString()));
            }
            else if (char.IsDigit(c))
            {
                this.Tokens.Add(new Token(TokenType.Digit, c.ToString()));
            }
            else if ("=".Contains(c))
            {
                this.Tokens.Add(new Token(TokenType.Operator, c.ToString()));
            }
            else
            {
                this.Tokens.Add(new Token(TokenType.Unknown, c.ToString()));
            }
        }
        return this.Tokens;
    }

    public List<Token> ScanFile(string filePath = "")
    {
        this.Tokens = new List<Token>();

        // Tokenization logic goes here

        return this.Tokens;
    }
}