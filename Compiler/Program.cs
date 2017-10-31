using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Variables.Integer.Add("a", 1);
            Variables.Integer.Add("b", 2);
            Variables.Integer.Add("num", 0);

            Compiler compiler = new Compiler(new Lexer(), new Parser());
            compiler.FilePath = @"..\..\Pascal\test.ps";
            //compiler.FilePath = @"..\..\Pascal\tt.ps";
            bool lex = compiler.CodeAnalysis();
            //if (lex)
            //{
            //    SyntaxError syntaxError;
            //    syntaxError = compiler.CheckSyntax();
            //    if (syntaxError == SyntaxError.NoError)
            //    {
            //        compiler.MakeSyntaxTree();
            //        compiler.Run();
            //    }
            //}


            Console.WriteLine("--------------------");

            IfSyntax ifsyntax = new IfSyntax("if a = b");
            ifsyntax.Elements[0] = "if";
            Console.WriteLine(ifsyntax.Check());

            Console.ReadKey();
        }
    }
}
