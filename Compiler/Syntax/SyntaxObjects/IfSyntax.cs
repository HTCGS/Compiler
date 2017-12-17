using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfSyntax : SyntaxObject
    {
        public IfSyntax()
        {
            this.Syntax = new List<ISyntaxElement>
            {
                new FunctionIdSyntax("If", "if"),
                new ExpressionSyntax(),
                new ArithmeticCondition(),
                new ExpressionSyntax(),
                new SymbolSyntax("", "then"),
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax(),
            };
            this.Elements = new string[Syntax.Count];
        }

        public IfSyntax(string line) : this()
        {
            this.Line = line;
        }

        public override IParserElement GetParser()
        {
            IfParser parser = new IfParser();
            parser.Line = Elements[1] + " " + Elements[2]
                + " " + Elements[3] + " " + Elements[5]
                + " " + Elements[7];
            return parser;
        }

    }
}
