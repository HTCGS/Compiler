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
            this.Syntax = new List<ISyntaxObject>
            {
                new FunctionIdSyntax("Write", "Write"),
                new BracketSyntax("Bracket", "("),
                new ExpressionSyntax(true),
                new BracketSyntax("Bracket", ")"),
            };
        }

        public WriteSyntax(string context) : base(context)
        {
        }

        public override IParserElement GetParser()
        {
            WriteParser writeParser = new WriteParser();
            writeParser.Line = Elements.GetParserElements(2);
            return writeParser;
        }

    }
}
