using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public abstract class AbstractLexerElement : ILexerElement
    {
        public string Key { get; set; }

        public AbstractLexerElement()
        {
            this.Key = string.Empty;
        }

        public AbstractLexerElement(string key) : this()
        {
            this.Key = key;
        }

        public abstract Keyword GetKeyword(string input);
        public abstract ISyntaxObject GetSyntaxScaner();
    }
}