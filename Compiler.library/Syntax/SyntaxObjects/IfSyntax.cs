using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class IfSyntax : SyntaxObject
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
                //new IdSyntax(),
                //new AssignSyntax("Assign", ":="),
                //new ExpressionSyntax(),
                //new VariableSyntax(),
                new IFTrueBlockSyntax(),
                new ElseBlockSyntax(true)
            };
        }

        public IfSyntax(string context) : base(context) { }

        public override IParserElement GetParser()
        {
            IfParser parser = new IfParser();
            //parser.Line = Elements.GetParserElements(1, 2, 3, 5, 7, 8);
            parser.Line = Elements.GetParserElements(1, 2, 3, 5, 6);

            return parser;
        }

    }
}