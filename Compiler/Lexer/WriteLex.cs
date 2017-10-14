using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteLex : ILexerElement
    {
        public string Key { get; private set; }

        public WriteLex()
        {
            this.Key = "Write";
        }

        public WriteLex(string key)
        {
            this.Key = key;
        }

        public Keyword GetKeyword(string input)
        {
            return Keyword.Function;
        }
    }
}
