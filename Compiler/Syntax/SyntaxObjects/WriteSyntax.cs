using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteSyntax : SyntaxObject
    {
        public WriteSyntax()
        {
            this.Syntax = new List<ISyntaxElement>
            {
                new FunctionIdSyntax("Write", "Write"),
                new BracketSyntax("Bracket", "("),
                new IdSyntax(),
                new BracketSyntax("Bracket", ")")
            };
            this.Elements = new string[Syntax.Count];
        }

        public WriteSyntax(string line) : this()
        {
            this.Line = line;
        }

        public override IParserElement GetParser()
        {
            WriteParser writeParser = new WriteParser();
            writeParser.Line = Elements[2];
            return writeParser;
        }

    }
}
