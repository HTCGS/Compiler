using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CompilerBanchmark
{
    public class Program
    {
        [Benchmark]
        public void Compiler_V1()
        {
            Compiler.Compiler compiler = new Compiler.Compiler(new Compiler.Lexer(), new Compiler.Parser());
            compiler.FilePath = Path.Combine(AppContext.BaseDirectory, @"./Test-01.txt");
            bool lex = compiler.CodeAnalysis();
            if (lex)
            {
                Compiler.SyntaxError syntaxError;
                syntaxError = compiler.CheckSyntax();
                if (syntaxError == Compiler.SyntaxError.NoError)
                {
                    compiler.MakeSyntaxTree();
                    // compiler.Run();
                }
            }
        }

        [Benchmark]
        public void Compiler_V2()
        {
            var lexer = new CompilerV2.Lexer();
            lexer.Keywords = new List<string> { "if", "then", "else", "write" };

            var compiler = new CompilerV2.Compiler(lexer);
            compiler.ScanFile(Path.Combine(AppContext.BaseDirectory, @"./Test-02.txt"))
                    .ParseTokens()
                    .ParseAST();
            //         .ExecuteCode();
        }

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Program>();
            // new Program().Compiler_V1();
            // new Program().Compiler_V2();
        }
    }
}