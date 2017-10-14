﻿using System;
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
            Console.WriteLine(variableLex.GetKeyword("a"));

            SymbolLex symbolLex = new SymbolLex();
            Console.WriteLine(symbolLex.GetKeyword("deadsdsa"));

            //ExpressionSyntax expressionSyntax = new ExpressionSyntax("Expression");
            //Console.WriteLine(expressionSyntax.Check("123"));

            VariableSyntax variableSyntax = new VariableSyntax(@"a : = 1+2");
            variableSyntax.Elements[0] = "a";
            Console.WriteLine(variableSyntax.Check());
            foreach (var item in variableSyntax.Elements)
            {
                Console.WriteLine(item);
            }

            WriteSyntax writeSyntax = new WriteSyntax("a := 1 + 1 +# a -a/a bb");
            writeSyntax.Elements[0] = "a";
            Console.WriteLine(writeSyntax.Check());
            foreach (var item in writeSyntax.Elements)
            {
                Console.WriteLine(item);
            }



            Console.ReadKey();
        }
    }
}