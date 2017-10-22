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
            Variables.Boolean.Add("a", true);
            Variables.Boolean.Add("b", true);
            Variables.Integer.Add("num", 0);


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
                syntaxObject.Check();
            }

            //ExpressionParser expressionParser = new ExpressionParser("(-b-(-b))-a+2*-a/(-a)");
            ExpressionParser expressionParser = new ExpressionParser("-a-a-b-b-a-2-b-5");
            Console.WriteLine(expressionParser.Line);
            Console.WriteLine(expressionParser.Check());
            expressionParser.Normalize();
            Console.WriteLine(expressionParser.Line);
            Console.WriteLine(expressionParser.Check());


            Console.ReadKey();
        }
    }
}
