using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Compiler
{
    class Lexer :ILexer
    {
        public string FilePath { get; set; }
        public string[] Code { get; set; }

        private int Line;

        public List<ILexerElement> LexElements { get; set; }

        public Lexer()
        {
            LexElements = new List<ILexerElement>()
            {
                new SymbolLex(),
                new VariableLex(),
                new WriteLex(),
                new WritelnLex()
            };
            Line = -1;
        }

        public Lexer(string path) : this()
        {
            this.FilePath = path;
        }

        public string[] ReadFile(string path)
        {
            Code = File.ReadAllLines(path);
            return Code;
        }

        public ISyntaxObject ScanFile()
        {
            Line++;
            string text = string.Empty;
            ISyntaxObject syntaxObject = null;
            for (int i = 1; i <= Code[Line].Length; i++)
            {
                text = Code[Line].Substring(0, i);
                foreach (var element in LexElements)
                {
                    if (element.GetKeyword(text) != Keyword.Unknown
                        && element.GetKeyword(text) != Keyword.LanguageSymbols)
                    {
                        syntaxObject = element.GetSyntaxScaner();
                        syntaxObject.Line = Code[Line];
                        syntaxObject.Elements[0] = text;
                    }
                }
            }
            if(text != string.Empty) return syntaxObject;
            return new EmptySyntax();
        }
    }
}
