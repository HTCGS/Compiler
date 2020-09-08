using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class Lexer : ILexer
    {
        public List<string> Keywods { get; set; }
        public List<List<Lexeme>> LexTable { get; set; }
        public List<string> ProgramText { get; set; }

        public List<List<Lexeme>> CreateLexTable(List<string> code)
        {
            List<Lexeme> lexemes;
            foreach (var line in code)
            {
                lexemes = new List<Lexeme>();
                string text = string.Empty;
                bool isDigit = false;
                bool isLetter  = false;
                foreach (var symbol in line)
                {
                    text += symbol;
                    if(char.IsDigit(symbol))
                    {
                        if(isLetter)
                        {
                            
                        }
                    }
                }

            }
            return this.LexTable;
        }
    }
    // public class Lexer : ILexer
    // {
    //     public string FilePath { get; set; }
    //     public string[] Code { get; set; }

    //     private int Line;

    //     public List<ILexerElement> LexElements { get; set; }

    //     public Lexer()
    //     {
    //         LexElements = new List<ILexerElement>()
    //         {
    //             new SymbolLex(),
    //             new VariableLex(),
    //             new WriteLex(),
    //             new WritelnLex(),
    //             new IfLex(),
    //             new CommentLex()
    //         };
    //         Line = -1;
    //     }

    //     public Lexer(string path) : this()
    //     {
    //         this.FilePath = path;
    //     }

    //     public string[] ReadFile(string path)
    //     {
    //         Code = File.ReadAllLines(path);
    //         return Code;
    //     }

    //     public ISyntaxObject ScanFile()
    //     {
    //         Line++;
    //         string text = string.Empty;
    //         ISyntaxObject syntaxObject = null;
    //         for (int i = 1; i <= Code[Line].Length; i++)
    //         {
    //             text = Code[Line].Substring(0, i);
    //             text = text.Replace(" ", string.Empty);
    //             foreach (var element in LexElements)
    //             {
    //                 Keyword keyword = element.GetKeyword(text);
    //                 if (keyword != Keyword.Unknown &&
    //                     keyword != Keyword.LanguageSymbols)
    //                 {
    //                     syntaxObject = element.GetSyntaxScaner();
    //                     syntaxObject.Context = Code[Line];
    //                     if (syntaxObject.Elements.ElementCount == 0)
    //                     {
    //                         syntaxObject.Elements.Add(text);
    //                     }
    //                 }
    //             }
    //         }
    //         if (text != string.Empty) return syntaxObject;
    //         return new EmptySyntax();
    //     }
    // }
}