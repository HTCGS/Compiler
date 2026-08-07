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

        string lexeme = string.Empty;
        var currentTokenType = TokenType.Unknown;

        foreach (char chr in this.Source)
        {

            if ("abcde".Contains(chr))
            {
                if (currentTokenType != TokenType.Letter && !string.IsNullOrEmpty(lexeme))
                {
                    this.Tokens.Add(new Token(currentTokenType, lexeme));
                    lexeme = string.Empty;
                }
                currentTokenType = TokenType.Letter;
                lexeme += chr;
            }
            else if (char.IsDigit(chr))
            {
                if (currentTokenType != TokenType.Digit && !string.IsNullOrEmpty(lexeme))
                {
                    this.Tokens.Add(new Token(currentTokenType, lexeme));
                    lexeme = string.Empty;
                }
                currentTokenType = TokenType.Digit;
                lexeme += chr;
            }
            else if ("=+-*/".Contains(chr))
            {
                if (currentTokenType != TokenType.Operator && !string.IsNullOrEmpty(lexeme))
                {
                    this.Tokens.Add(new Token(currentTokenType, lexeme));
                    lexeme = string.Empty;
                }
                this.Tokens.Add(new Token(TokenType.Operator, chr.ToString()));
                currentTokenType = TokenType.Unknown;
                lexeme = string.Empty;
            }
            else if (char.IsWhiteSpace(chr))
            {
                if (!string.IsNullOrEmpty(lexeme))
                {
                    this.Tokens.Add(new Token(currentTokenType, lexeme));
                    lexeme = string.Empty;
                }
                currentTokenType = TokenType.Unknown;
            }
            else
            {
                if (!string.IsNullOrEmpty(lexeme))
                {
                    this.Tokens.Add(new Token(currentTokenType, lexeme));
                    lexeme = string.Empty;
                }
                currentTokenType = TokenType.Unknown;
                this.Tokens.Add(new Token(currentTokenType, chr.ToString()));
            }
        }
        if (!string.IsNullOrEmpty(lexeme))
        {
            this.Tokens.Add(new Token(currentTokenType, lexeme));
        }
        return this.Tokens;
    }

    public List<Token> Scan(string source)
    {
        this.Source = source;
        var tokens = this.Scan();
        return tokens;
    }

    public List<Token> ScanFile(string filePath = "")
    {
        this.Tokens = new List<Token>();

        // Tokenization logic goes here

        return this.Tokens;
    }
}