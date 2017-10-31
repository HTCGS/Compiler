using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnSyntax : SyntaxObject
    {
        public WritelnSyntax()
        {
            this.Syntax = new List<ISyntaxElement>
            {
                new FunctionIdSyntax("Write", "Writeln"),
                new BracketSyntax("Bracket", "("),
                new ExpressionSyntax(),
                new BracketSyntax("Bracket", ")")
            };
            this.Elements = new string[Syntax.Count];
        }

        public WritelnSyntax(string line) : this()
        {
            this.Line = line;
        }

        public override IParserElement GetParser()
        {
            WritelnParser writelnParser = new WritelnParser();
            writelnParser.Line = Elements[2];
            return writelnParser;
        }
    }
}
