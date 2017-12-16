﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfLex : AbstractLexerElement
    {
        public IfLex()
        {
            this.Key = "if";
        }

        public IfLex(string key) : base(key)
        {
        }

        public override Keyword GetKeyword(string input)
        {
            if (input == Key) return Keyword.Key;
            return Keyword.Unknown;
        }

        public override ISyntaxObject GetSyntaxScaner()
        {
            IfSyntax syntax = new IfSyntax();
            syntax.Elements[0] = this.Key;
            return syntax;
        }
    }
}