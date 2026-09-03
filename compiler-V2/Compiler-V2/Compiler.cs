using System.Linq.Expressions;

public class Compiler
{
    public Lexer Lexer;
    public TokenParser TokenParser;
    public ASTParser AstParser;

    public List<List<Token>> TokenTable;
    public List<SyntaxNode> AST;
    public List<Expression> Code;

    public Compiler()
    {
        this.Lexer = new Lexer();
        this.TokenParser = new TokenParser();
        this.AstParser = new ASTParser();
        this.TokenTable = new List<List<Token>>();
        this.AST = new List<SyntaxNode>();
        this.Code = new List<Expression>();
    }

    public Compiler(Lexer lexer) : this()
    {
        this.Lexer = lexer;
    }

    public Compiler(Lexer lexer, TokenParser tokenParser)
    {
        this.Lexer = lexer;
        this.TokenParser = tokenParser;
    }

    public Compiler Scan(string source)
    {
        var tokens = Lexer.Scan(source);
        if (tokens.Count == 1 && tokens[0].Type == TokenType.Unknown)
        {
            this.TokenTable.Clear();
            System.Console.WriteLine($"Error: Invalid token found: '{tokens[0].Lexeme}'");
            return null;
        }
        this.TokenTable.Add(tokens);
        return this;
    }

    public Compiler ScanFile(string filePath)
    {
        var text = File.ReadAllLines(System.IO.Path.GetFullPath(filePath));
        if (text.Length != 0)
        {
            int lineNumber = 0;
            foreach (var line in text)
            {
                lineNumber++;
                if (line.Count() == 0) continue;
                var tokenLine = Lexer.Scan(line);
                if (tokenLine.Count == 1 && tokenLine[0].Type == TokenType.Unknown)
                {
                    this.TokenTable.Clear();
                    System.Console.WriteLine($"Error: Invalid token found in line {lineNumber}: '{tokenLine[0].Lexeme}'");
                    return null;
                }
                this.TokenTable.Add(tokenLine);
            }
        }
        return this;
    }

    public Compiler ParseTokens()
    {
        foreach (var lineOfTokens in this.TokenTable)
        {
            var expr = TokenParser.Parse(lineOfTokens);
            if (expr is not UnknownSyntax) this.AST.Add(expr);
            else
            {
                System.Console.WriteLine(expr.Name);
                this.AST.Clear();
                this.AST.Add(expr);
            }
        }
        return this;
    }

    public Compiler ParseAST()
    {
        foreach (var astElement in this.AST)
        {
            var expr = AstParser.Parse(astElement);
            this.Code.Add(expr);
        }
        return this;
    }

    public Compiler ExecuteCode()
    {
        try
        {
            var allVariables = VariableManager.Variables.Select(kvp => kvp.Value).ToList();
            var program = Expression.Block(allVariables, this.Code);
            var compiledProgram = Expression.Lambda<Action>(program).Compile();
            compiledProgram();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return this;
    }


}