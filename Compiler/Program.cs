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

            ExpressionParser expressionParser = new ExpressionParser("1*3+4+8");
            //ExpressionParser expressionParser = new ExpressionParser("(-2)-4");
            //ExpressionParser expressionParser = new ExpressionParser("(-1)+3*(2/(-2))");
            //ExpressionParser expressionParser = new ExpressionParser("(-b-(-b))-a+2*a/(-a)");
            //ExpressionParser expressionParser = new ExpressionParser("(-b-1)"); //error ???
            //ExpressionParser expressionParser = new ExpressionParser("-a-a-1-a-4-a-b");
            Console.WriteLine(expressionParser.Line);
            Console.WriteLine(expressionParser.Check());
            expressionParser.Normalize();
            Console.WriteLine(expressionParser.Line);
            Console.WriteLine(expressionParser.Check());
            ISyntaxTree syntaxTree = expressionParser.GetSyntaxTree(expressionParser.Line);
            Console.WriteLine(syntaxTree.Execute());



            Console.ReadKey();
        }
    }
}
