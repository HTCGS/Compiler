using System;
using System.IO;
using Compiler;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Compiler.Compiler compiler = new Compiler.Compiler(new Lexer(), new Parser());
            compiler.FilePath = Path.Combine(AppContext.BaseDirectory, "Pascal", "test.ps");
            bool lex = compiler.CodeAnalysis();
            if (lex)
            {
                SyntaxError syntaxError;
                syntaxError = compiler.CheckSyntax();
                if (syntaxError == SyntaxError.NoError)
                {
                    compiler.MakeSyntaxTree();
                    compiler.Run();
                }
            }

            // Console.ReadKey();
        }
    }
}