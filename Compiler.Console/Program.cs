using System;
using System.Collections.Generic;
using CompilerLib;

namespace Compiler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var keyword = new List<string>()
            {
                "Write"
            };

            var programText = new List<string>
            {
                "11 4Write()az"
            };

            var lexer = new Lexer(keyword);
            var lexTable = lexer.CreateLexTable(programText);

            foreach (var row in lexTable)
            {
                foreach (var item in row)
                {
                    System.Console.WriteLine($"{item.Token}: {item.LexemeType}");
                }
            }
            // System.Console.ReadKey();
        }
    }
}
