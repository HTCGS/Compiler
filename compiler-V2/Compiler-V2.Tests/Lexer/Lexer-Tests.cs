public class Lexer_Tests
{
    [Fact]
    public void Scan_Digits_ShouldReturnTokensForDigits()
    {
        // Arrange
        string source = "12345";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(5, tokens.Count);
        Assert.All(tokens, token => Assert.Equal(TokenType.Digit, token.Type));
    }

    [Fact]
    public void Scan_Letters_ShouldReturnTokensForLetters()
    {
        // Arrange
        string source = "abcde";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(5, tokens.Count);
        Assert.All(tokens, token => Assert.Equal(TokenType.Letter, token.Type));
    }

    [Fact]
    public void Scan_AssignmentOperator_ShouldReturnTokensForOperators()
    {
        // Arrange
        string source = "=";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Single(tokens);
        Assert.All(tokens, token => Assert.Equal(TokenType.Operator, token.Type));
    }

    [Fact]
    public void Scan_AssignmentDigitToLetter_ReturnTokensForLettersOperatorsAndDigits()
    {
        // Arrange
        string source = "a=1";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Digit, tokens[2].Type);
    }

    [Fact]
    public void Scan_AssignmentLetterToLetter_ReturnTokensForLettersOperatorsAndLetters()
    {
        // Arrange
        string source = "a=b";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Letter, tokens[2].Type);
    }

    [Fact]
    public void Scan_EmptySource_ShouldReturnEmptyTokenList()
    {
        // Arrange
        string source = "";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Empty(tokens);
    }

    [Fact]
    public void Scan_ShouldReturnUnknownTokenForSpecialCharacters()
    {
        // Arrange
        string source = "@#$";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.All(tokens, token => Assert.Equal(TokenType.Unknown, token.Type));
    }
}