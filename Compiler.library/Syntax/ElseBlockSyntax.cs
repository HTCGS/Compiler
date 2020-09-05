using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ElseBlockSyntax : SyntaxObject
    {
        public ElseBlockSyntax()
        {
            Syntax = new List<ISyntaxObject>
            {
                new SymbolSyntax("", "else"),
                new AnySyntaxObject()
            };
        }

        public ElseBlockSyntax(string context) : this() //: base(context)
        {
            this.Context = context;
        }

        public ElseBlockSyntax(bool isNullable) : this() //: base(isNullable)
        {
            this.IsNullable = isNullable;
        }

        public override IParserElement GetParser()
        {
            return Elements.Elements[1].ElementReference.GetParser();

        }
    }
}