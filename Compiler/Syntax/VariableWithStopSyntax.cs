using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableWithStopSyntax : SyntaxObject
    {
        public VariableWithStopSyntax()
        {
            Syntax = new List<ISyntaxObject>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression"),
                new SymbolSyntax("", true, "else")
            };
        }

        public VariableWithStopSyntax(string context) : base(context)
        {
        }

        public VariableWithStopSyntax(bool isNullable) : base(isNullable)
        {
        }

        public override SyntaxError Check(string context)
        {
            SyntaxError check = base.Check(context);
            if (check == SyntaxError.NoError)
            {
                Elements.Elements.RemoveAt(3);
            }
            this.Context = Elements.Context;
            return check;
        }

        public override IParserElement GetParser()
        {
            VariableParser parser = new VariableParser();
            parser.Line = Elements.GetParserElements(0, 2);
            return parser;
        }
    }
}
