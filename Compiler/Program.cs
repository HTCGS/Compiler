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
            VariableLex variableLex = new VariableLex();
            Variables.Boolean.Add("a", true);
            Variables.Boolean.Add("b", true);


            VariableSyntax variableSyntax = new VariableSyntax(@"a : = (-1-1  + 2 ) ");
            variableSyntax.Elements[0] = "a";
            Console.WriteLine(variableSyntax.Check());
            foreach (var item in variableSyntax.Elements)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();


            Lexer lexer = new Lexer(@"..\..\Pascal\test.ps");
            lexer.ReadFile(lexer.FilePath);
            foreach (var item in lexer.Code)
            {
                ISyntaxObject syntaxObject = lexer.ScanFile();
                Console.WriteLine(syntaxObject);
            }



            Console.ReadKey();
        }
    }
}
