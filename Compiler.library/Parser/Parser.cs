using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Parser : IParser
    {
        public List<IParserElement> ParserElements { get; set; }

        public List<ISyntaxObject> Syntax { get; set; }

        public Parser()
        {
            ParserElements = new List<IParserElement>();
        }

        public Parser(List<ISyntaxObject> syntax) : this()
        {
            this.Syntax = syntax;
        }

        public SyntaxError Check()
        {
            foreach (ISyntaxObject item in Syntax)
            {
                if (!(item is EmptySyntax))
                {
                    //SyntaxError syntaxError = item.Check();
                    //if (syntaxError != SyntaxError.NoError) return syntaxError;
                    ParserElements.Add(item.GetParser());
                }
            }
            return SyntaxError.NoError;
        }

    }
}