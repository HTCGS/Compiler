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
            Syntax = new List<ISyntaxObject>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression")
            };
        }

        public VariableSyntax(string context) : base(context)
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
