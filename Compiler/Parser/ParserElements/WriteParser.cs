﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteParser : IParserElement
    {
        public string Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            dynamic context = Variables.GetVariable(text);
            return new WriteTree(context);
        }

        public void Normalize()
        {

        }
    }
}
