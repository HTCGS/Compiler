using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EmptySyntax : ISyntaxObject
    {
        public List<ISyntaxElement> Syntax { get; set; }
        public string Line { get; set; }
        public string[] Elements { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public IParserElement GetParser()
        {
            return null;
        }
    }
}
