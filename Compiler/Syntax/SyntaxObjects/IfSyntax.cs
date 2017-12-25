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
            this.Syntax = new List<ISyntaxObject>
            {
                new FunctionIdSyntax("If", "if"),
                new ExpressionSyntax(),
                new ArithmeticConditionSymbol(),
                new ExpressionSyntax(),
                new SymbolSyntax("", "then"),
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax(),
                //new SymbolSyntax("", true, "else"),
                //new IdSyntax(),
                //new AssignSyntax("Assign", ":="),
                //new ExpressionSyntax(),
            };
        }

        public IfSyntax(string context) : base(context)
        {
        }

        public override IParserElement GetParser()
        {
            IfParser parser = new IfParser();
            parser.Line = Elements[1] + " " + Elements[2]
                + " " + Elements[3] + " " + Elements[5]
                + " " + Elements[7];
            if (this.Elements.ElementCount > 8)
            {
                parser.Line += " " + Elements[9] + " " + Elements[11];
            }
            return parser;
        }

    }
}
