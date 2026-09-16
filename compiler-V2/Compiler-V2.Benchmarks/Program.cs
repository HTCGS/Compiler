using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


public class CompilerBenchmarks
{

    [Benchmark]
    public void Compiler_V1()
    {
        Compiler.Variables.Integer.Add("a", 0);
        Compiler.Variables.Integer.Add("b", 0);
        Compiler.Variables.Integer.Add("num", 10);

        Compiler.Compiler compiler = new Compiler.Compiler(new Compiler.Lexer(), new Compiler.Parser());
        compiler.FilePath = Path.Combine(AppContext.BaseDirectory, @"../../../Test-01.txt");
        bool lex = compiler.CodeAnalysis();
        if (lex)
        {
            Compiler.SyntaxError syntaxError;
            syntaxError = compiler.CheckSyntax();
            if (syntaxError == Compiler.SyntaxError.NoError)
            {
                compiler.MakeSyntaxTree();
                compiler.Run();
            }
        }
    }
    [Benchmark]
    public void Compiler_V2()
    {
        var lexer = new CompilerV2.Lexer();
        lexer.Keywords = new List<string> { "if", "then", "else", "write" };

        var compiler = new CompilerV2.Compiler(lexer);
        // compiler.ScanFile(Path.Combine(AppContext.BaseDirectory, @"../../../Test-01.txt"))
        //         .ParseTokens()
        //         .ParseAST()
        //         .ExecuteCode(); ;
    }

}
// Compiler_V1();
// Compiler_V2();

// var summary = BenchmarkRunner.Run<CompilerBenchmarks>();