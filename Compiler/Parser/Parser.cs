using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Parser : IParser
    {
        public List<IParserElement> ParserElements { get; set; }

        public List<ISyntaxObject> Syntax { get; set; }

        public Parser()
        {
            ParserElements = new List<IParserElement>
            {

            };
        }

        public Parser(List<ISyntaxObject> syntax ) :this()
        {
            this.Syntax = syntax;
        }

        public SyntaxError Check()
        {



            return SyntaxError.NoError;
        }
    }
}
