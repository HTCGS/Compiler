using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnLex : ILexerElement
    {
        public string Key { get; private set; }

        public WritelnLex()
        {
            this.Key = "Writeln";
        }

        public WritelnLex(string key)
        {
            this.Key = key;
        }

        public Keyword GetKeyword(string input)
        {
            if (input == Key) return Keyword.Function;
            return Keyword.Unknown;
        }

        public ISyntaxObject GetSyntaxScaner()
        {
            ISyntaxObject syntaxObject = new WritelnSyntax();
            syntaxObject.Elements[0] = this.Key;
            return syntaxObject;
        }
    }
}
