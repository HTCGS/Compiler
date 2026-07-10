class Lexer
{
    public string Source { get; set; }

    public Lexer(string source)
    {
        this.Source = source;
    }

    public List<Lexeme> Scan()
    {
        List<Lexeme> lexemes = new List<Lexeme>();

        // Tokenization logic goes here

        return lexemes;
    }

    public List<Lexeme> ScanFile()
    {
        List<Lexeme> lexemes = new List<Lexeme>();

        // Tokenization logic goes here

        return lexemes;
    }
}