public class Lexer_Tests
{
    [Fact]
    public void Scan_EmptySource_ReturnsEmptyTokenList()
    {
        // Arrange
        string source = "";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Empty(tokens);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("    ")]
    [InlineData("     ")]
    public void Scan_Whitespaces_ReturnsEmptyTokenList(string whitespaces)
    {
        // Arrange
        string source = whitespaces;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Empty(tokens);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]
    [InlineData("9")]
    public void Scan_SingleDigit_ReturnsSingleDigitToken(string digit)
    {
        // Arrange
        string source = digit;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Single(tokens);
        Assert.Equal(TokenType.Digit, tokens[0].Type);
        Assert.Equal(digit, tokens[0].Lexeme);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("b")]
    [InlineData("c")]
    [InlineData("d")]
    [InlineData("e")]
    public void Scan_SingleLetter_ReturnsSingleLetterToken(string letter)
    {
        // Arrange
        string source = letter;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Single(tokens);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(letter, tokens[0].Lexeme);
    }

    [Theory]
    [InlineData("=")]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    public void Scan_SingleOperator_ReturnsSingleOperatorToken(string operatorSymbol)
    {
        // Arrange
        string source = operatorSymbol;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Single(tokens);
        Assert.Equal(TokenType.Operator, tokens[0].Type);
        Assert.Equal(operatorSymbol, tokens[0].Lexeme);
    }

    [Theory]
    [InlineData("1 1")]
    [InlineData("3 3")]
    [InlineData("5 5")]
    [InlineData("1 2")]
    [InlineData("3 4")]
    [InlineData("5 6")]
    [InlineData("7 8")]
    [InlineData("9 0")]
    public void Scan_TwoDigitsWithSpace_ReturnsTwoDigitTokens(string digits)
    {
        // Arrange
        string source = digits;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.All(tokens, token => Assert.Equal(TokenType.Digit, token.Type));
    }

    [Theory]
    [InlineData("a a")]
    [InlineData("b b")]
    [InlineData("c c")]
    [InlineData("d d")]
    [InlineData("e e")]
    [InlineData("a b")]
    [InlineData("b c")]
    [InlineData("c d")]
    [InlineData("d e")]
    public void Scan_TwoLettersWithSpace_ReturnsTwoLetterTokens(string letters)
    {
        // Arrange
        string source = letters;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.All(tokens, token => Assert.Equal(TokenType.Letter, token.Type));
    }

    [Theory]
    [InlineData("a 1")]
    [InlineData("b 2")]
    [InlineData("c 3")]
    [InlineData("d 4")]
    [InlineData("e 5")]
    public void Scan_LetterAndDigitWithSpace_ReturnsLetterAndDigitTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Digit, tokens[1].Type);
    }

    [Theory]
    [InlineData("6 a")]
    [InlineData("7 b")]
    [InlineData("8 c")]
    [InlineData("9 d")]
    [InlineData("0 e")]
    public void Scan_DigitAndLetterWithSpace_ReturnsDigitAndLetterTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(2, tokens.Count);
        Assert.Equal(TokenType.Digit, tokens[0].Type);
        Assert.Equal(TokenType.Letter, tokens[1].Type);
    }

    [Theory]
    [InlineData("a=1")]
    [InlineData("b=2")]
    [InlineData("c=3")]
    [InlineData("d=4")]
    [InlineData("e=5")]
    public void Scan_LetterAssignDigit_ReturnsLetterOperatorDigitTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Digit, tokens[2].Type);
    }


    [Theory]
    [InlineData("a=a")]
    [InlineData("b=b")]
    [InlineData("c=c")]
    [InlineData("d=d")]
    [InlineData("e=e")]
    public void Scan_LetterAssignLetter_ReturnsLetterOperatorLetterTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Letter, tokens[2].Type);
    }


    [Theory]
    [InlineData("1+1")]
    [InlineData("2+2")]
    [InlineData("3+3")]
    [InlineData("4+4")]
    [InlineData("5+5")]
    public void Scan_DigitPlusDigit_ReturnsDigitOperatorDigitTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Digit, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Digit, tokens[2].Type);
    }

    [Theory]
    [InlineData("a+a")]
    [InlineData("b+b")]
    [InlineData("c+c")]
    [InlineData("d+d")]
    [InlineData("e+e")]
    public void Scan_LetterPlusLetter_ReturnsLetterOperatorLetterTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Letter, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Letter, tokens[2].Type);
    }

    [Theory]
    [InlineData("1+a")]
    [InlineData("2+b")]
    [InlineData("3+c")]
    [InlineData("4+d")]
    [InlineData("5+e")]
    public void Scan_DigitPlusLetter_ReturnsDigitOperatorLetterTokens(string value)
    {
        // Arrange
        string source = value;
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Equal(3, tokens.Count);
        Assert.Equal(TokenType.Digit, tokens[0].Type);
        Assert.Equal(TokenType.Operator, tokens[1].Type);
        Assert.Equal(TokenType.Letter, tokens[2].Type);
    }

    [Theory]
    [InlineData("a+6")]
    [InlineData("b+7")]
    [InlineData("c+8")]
    [InlineData("d+9")]
    [InlineData("e+0")]
    public void Scan_LetterPlusDigit_ReturnsLetterOperatorDigitTokens(string value)
    {
        // Arrange
        string source = value;
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
    public void Scan_InvalidCharacters_ReturnUnknownTokens()
    {
        // Arrange
        string source = "@#$";
        Lexer lexer = new Lexer(source);

        // Act
        List<Token> tokens = lexer.Scan();

        // Assert
        Assert.Empty(tokens);
        Assert.All(tokens, token => Assert.Equal(TokenType.Unknown, token.Type));
    }
}