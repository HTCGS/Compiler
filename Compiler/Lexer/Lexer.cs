using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Compiler
{
    class Lexer
    {
        public string FilePath { get; set; }
        public string[] Code { get; set; }

        private int Line;

        public List<ILexerElement> LexElements;

        public Lexer()
        {
            LexElements = new List<ILexerElement>()
            {
                new SymbolLex(),
                new VariableLex(),
                new WriteLex()
            };
            Line = -1;
        }

        public Lexer(string path) : this()
        {
            this.FilePath = path;
        }

        public void ReadFile(string path)
        {
            Code = File.ReadAllLines(path);
        }

        public ISyntaxObject ScanFile()
        {
            Line++;
            string text = string.Empty;
            for (int i = 1; i <= Code[Line].Length; i++)
            {
                text = Code[Line].Substring(0, i);
                foreach (var element in LexElements)
                {
                    if(element.GetKeyword(text) != Keyword.Unknown 
                        && element.GetKeyword(text) != Keyword.LanguageSymbols)
                    {
                        return element.GetSyntaxScaner();
                    }
                }
            }
            return null;
        }
    }
}
