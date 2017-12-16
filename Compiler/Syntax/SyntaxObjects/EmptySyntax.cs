using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EmptySyntax : SyntaxObject
    {
        public EmptySyntax()
        {
            Elements = new string[1];
            Elements[0] = string.Empty;
            Syntax = new List<ISyntaxElement>();
        }

        public EmptySyntax(string line) : base(line)
        {
        }

        public override SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public override IParserElement GetParser()
        {
            return null;
        }
    }
}
