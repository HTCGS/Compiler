using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class CommentLex : AbstractLexerElement
    {
        public CommentLex()
        {
            this.Key = "//";
        }

        public CommentLex(string key) : base(key)
        {
        }

        public override Keyword GetKeyword(string input)
        {
            if(input == Key) return Keyword.Comment;
            return Keyword.Unknown;
        }

        public override ISyntaxObject GetSyntaxScaner()
        {
            return new EmptySyntax();
        }
    }
}
