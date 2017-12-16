﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteLex : AbstractLexerElement
    {
        public WriteLex()
        {
            this.Key = "Write";
        }

        public WriteLex(string key) : base(key)
        {
        }

        public override Keyword GetKeyword(string input)
        {
            if (input == Key) return Keyword.Function;
            return Keyword.Unknown;
        }

        public override ISyntaxObject GetSyntaxScaner()
        {
            ISyntaxObject syntaxObject = new WriteSyntax();
            syntaxObject.Elements[0] = this.Key;
            return syntaxObject;
        }
    }
}
