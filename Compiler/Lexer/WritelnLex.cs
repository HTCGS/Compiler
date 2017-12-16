using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnLex : AbstractLexerElement
    {
        public WritelnLex()
        {
            this.Key = "Writeln";
        }

        public WritelnLex(string key) : base(key)
        {
        }

        public override Keyword GetKeyword(string input)
        {
            if (input == Key) return Keyword.Function;
            return Keyword.Unknown;
        }

        public override ISyntaxObject GetSyntaxScaner()
        {
            ISyntaxObject syntaxObject = new WritelnSyntax();
            syntaxObject.Elements[0] = this.Key;
            return syntaxObject;
        }
    }
}
