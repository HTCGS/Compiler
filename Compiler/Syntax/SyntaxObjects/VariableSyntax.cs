using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableSyntax : SyntaxObject
    {
        public VariableSyntax()
        {
            Syntax = new List<ISyntaxElement>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression")
            };
            Elements = new string[Syntax.Count];
        }

        public VariableSyntax(string line) : base(line)
        {
        }

        public override IParserElement GetParser()
        {
            VariableParser parser = new VariableParser();
            parser.Line = Elements[0] + " " + Elements[2];
            return parser;
        }
    }
}
