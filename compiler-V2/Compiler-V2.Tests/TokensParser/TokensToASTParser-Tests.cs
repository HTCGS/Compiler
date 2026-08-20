public class TokensToASTParser_Tests
{
    [Fact]
    public void Parse_EmptyTokens_ReturnUnknownSyntax()
    {
        var tokenParser = new TokensToASTParser();
        var tokens = new List<Token>();

        var result = tokenParser.Parse(tokens);

        Assert.IsType<UnknownSyntax>(result);
    }


    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("22")]
    [InlineData("333")]
    [InlineData("4444")]
    public void Parse_SingleDigitToken_ReturnConstant(string digit)
    {
        var lexer = new Lexer(digit);
        var digitToken = lexer.Scan();
        var tokenParser = new TokensToASTParser();

        var result = tokenParser.Parse(digitToken);

        Assert.NotNull(result);
        Assert.IsType<Constant>(result);
        Assert.Equal(int.Parse(digit), (result as Constant).Value);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("bb")]
    [InlineData("ccc")]
    [InlineData("dddd")]
    [InlineData("eeeee")]
    public void Parse_SingleLetterToken_ReturnVariable(string letter)
    {
        var lexer = new Lexer(letter);
        var digitToken = lexer.Scan();
        var tokenParser = new TokensToASTParser();

        var result = tokenParser.Parse(digitToken);

        Assert.NotNull(result);
        Assert.IsType<Variable>(result);
        Assert.Equal(letter, result.Name);
    }

    [Theory]
    [InlineData("0+0")]
    [InlineData("0+1")]
    [InlineData("1+1")]
    [InlineData("2+2")]
    [InlineData("5+7")]
    public void ParseExpression_AddTwoDigits_ReturnPlusNode(string input)
    {
        var lexer = new Lexer(input);
        var additionTokens = lexer.Scan();
        var tokenParser = new TokensToASTParser();

        var result = tokenParser.ParseExpression(additionTokens);

        Assert.NotNull(result);
        Assert.IsType<Plus>(result);
    }

    [Theory]
    [InlineData("0*0")]
    [InlineData("0*1")]
    [InlineData("1*0")]
    [InlineData("2*2")]
    [InlineData("4*5")]
    public void ParseExpression_MultiplyTwoDigits_ReturnMultiplyNode(string input)
    {
        var lexer = new Lexer(input);
        var multiplyTokens = lexer.Scan();
        var tokenParser = new TokensToASTParser();

        var result = tokenParser.ParseExpression(multiplyTokens);

        Assert.NotNull(result);
        Assert.IsType<Multiply>(result);
    }

}