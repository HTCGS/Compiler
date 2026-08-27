public class Lexer
{
    public string Source { get; set; }

    public List<string> Keywords { get; set; }

    public List<Token> Tokens { get; set; }

    public Lexer()
    {
        this.Source = string.Empty;
        this.Tokens = new List<Token>();
        this.Keywords = new List<string>();
    }

    public Lexer(string source) : this()
    {
        this.Source = source;
    }

    public List<Token> Scan()
    {
        this.Tokens = new List<Token>();

        string lexeme = string.Empty;
        var prevTokenType = TokenType.Unknown;
        var currentTokenType = TokenType.Unknown;

        for (int i = 0; i < this.Source.Count(); i++)
        {
            var chr = this.Source[i];
            if (char.IsWhiteSpace(chr))
            {
                if (!string.IsNullOrEmpty(lexeme))
                {
                    if (prevTokenType == TokenType.Letter && IsKeyword(lexeme))
                        this.Tokens.Add(new Token(TokenType.Keyword, lexeme));
                    else
                        this.Tokens.Add(new Token(prevTokenType, lexeme));
                    prevTokenType = TokenType.Unknown;
                    lexeme = string.Empty;
                }
                continue;
            }
            currentTokenType = GetTokenType(chr);
            if (currentTokenType == TokenType.Unknown) return new List<Token>();
            if (currentTokenType == prevTokenType)
            {
                if (currentTokenType != TokenType.Operator && currentTokenType != TokenType.Bracket)
                    lexeme += chr;
                else
                {
                    this.Tokens.Add(new Token(prevTokenType, lexeme));
                    this.Tokens.Add(new Token(currentTokenType, chr.ToString()));
                    prevTokenType = TokenType.Unknown;
                    lexeme = string.Empty;
                }
            }
            else
            {
                if (prevTokenType == TokenType.Unknown)
                {
                    prevTokenType = currentTokenType;
                    lexeme += chr;
                }
                else
                {
                    if (prevTokenType == TokenType.Letter && IsKeyword(lexeme))
                        this.Tokens.Add(new Token(TokenType.Keyword, lexeme));
                    else
                        this.Tokens.Add(new Token(prevTokenType, lexeme));
                    prevTokenType = currentTokenType;
                    lexeme = string.Empty + chr;
                }
            }
        }
        if (!string.IsNullOrEmpty(lexeme)) this.Tokens.Add(new Token(currentTokenType, lexeme));


        // foreach (char chr in this.Source)
        // {

        //     if ("abcde".Contains(chr))
        //     {
        //         if (currentTokenType != TokenType.Letter && !string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         currentTokenType = TokenType.Letter;
        //         lexeme += chr;
        //     }
        //     else if (char.IsDigit(chr))
        //     {
        //         if (currentTokenType != TokenType.Digit && !string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         currentTokenType = TokenType.Digit;
        //         lexeme += chr;
        //     }
        //     else if ("=+-*/".Contains(chr))
        //     {
        //         if (currentTokenType != TokenType.Operator && !string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         this.Tokens.Add(new Token(TokenType.Operator, chr.ToString()));
        //         currentTokenType = TokenType.Unknown;
        //         lexeme = string.Empty;
        //     }
        //     else if ("()".Contains(chr))
        //     {
        //         if (currentTokenType != TokenType.Bracket && !string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         this.Tokens.Add(new Token(TokenType.Bracket, chr.ToString()));
        //         currentTokenType = TokenType.Unknown;
        //         lexeme = string.Empty;
        //     }
        //     else if (char.IsWhiteSpace(chr))
        //     {
        //         if (!string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         currentTokenType = TokenType.Unknown;
        //     }
        //     else
        //     {
        //         if (!string.IsNullOrEmpty(lexeme))
        //         {
        //             this.Tokens.Add(new Token(currentTokenType, lexeme));
        //             lexeme = string.Empty;
        //         }
        //         currentTokenType = TokenType.Unknown;
        //         this.Tokens.Add(new Token(currentTokenType, chr.ToString()));
        //     }
        // }
        // if (!string.IsNullOrEmpty(lexeme))
        // {
        //     this.Tokens.Add(new Token(currentTokenType, lexeme));
        // }
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

    public TokenType GetTokenType(char chr)
    {
        var tokenType = TokenType.Unknown;

        if ("abcdeifwrthn".Contains(chr)) tokenType = TokenType.Letter;
        else if (char.IsDigit(chr)) tokenType = TokenType.Digit;
        else if ("=+-*/".Contains(chr)) tokenType = TokenType.Operator;
        else if ("()".Contains(chr)) tokenType = TokenType.Bracket;

        return tokenType;
    }

    public bool IsKeyword(string lexeme)
    {
        if (this.Keywords.Contains(lexeme)) return true;
        return false;
    }
}