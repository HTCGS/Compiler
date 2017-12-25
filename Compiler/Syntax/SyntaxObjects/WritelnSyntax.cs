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
            this.Syntax = new List<ISyntaxObject>
            {
                new FunctionIdSyntax("Write", "Writeln"),
                new BracketSyntax("Bracket", "("),
                new ExpressionSyntax(true),
                new BracketSyntax("Bracket", ")"),
            };
        }

        public WritelnSyntax(string context) : base(context)
        {
        }

        public override IParserElement GetParser()
        {
            WritelnParser writelnParser = new WritelnParser();
            writelnParser.Line = Elements[2];
            return writelnParser;
        }
    }
}
