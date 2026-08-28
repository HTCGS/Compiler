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

    public List<Token> Scan(string source)
    {
        var tokens = Lexer.Scan(source);
        return tokens;
    }

    public List<List<Token>> ScanFile(string filePath)
    {
        var tokens = new List<List<Token>>();
        var text = File.ReadAllLines(System.IO.Path.GetFullPath(filePath));
        if (text.Length != 0)
        {
            foreach (var line in text)
            {
                var tokenLine = Lexer.Scan(line);
                if (tokenLine.Count != 0) this.TokenTable.Add(tokenLine);
            }
        }
        return tokens;
    }

    public List<SyntaxNode> ParseTokens()
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
                return new List<SyntaxNode> { expr };
            }
        }
        return this.AST;
    }

    public List<Expression> ParseAST()
    {
        foreach (var astElement in this.AST)
        {
            var expr = AstParser.Parse(astElement);
            this.Code.Add(expr);
        }
        return this.Code;
    }

    public void ExecuteCode()
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
    }


}