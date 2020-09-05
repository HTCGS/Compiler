using System;
using Compiler;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Variables.Integer.Add("a", 0);
            Variables.Integer.Add("b", 0);
            Variables.Integer.Add("num", 10);

            Compiler.Compiler compiler = new Compiler.Compiler(new Lexer(), new Parser());
            compiler.FilePath = @"Pascal\test.ps";
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

            Console.ReadKey();
        }
    }
}