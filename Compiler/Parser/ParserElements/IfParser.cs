using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfParser : IParserElement
    {
        public string Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            return new WriteTree(text);



        }

        public void Normalize()
        {

        }
    }
}
