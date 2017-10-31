using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfLex : ILexerElement
    {
        public string Key { get; private set; }

        public IfLex()
        {
            this.Key = "if";
        }

        public IfLex(string key)
        {
            this.Key = key;
        }

        public Keyword GetKeyword(string input)
        {
            if (input == Key) return Keyword.Key;
            return Keyword.Unknown;
        }

        ISyntaxObject ILexerElement.GetSyntaxScaner()
        {
            IfSyntax syntax = new IfSyntax();
            syntax.Elements[0] = this.Key;
            return syntax;
        }
    }
}
