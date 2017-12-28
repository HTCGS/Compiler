using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IFTrueBlockSyntax : SyntaxObject
    {
        public IFTrueBlockSyntax()
        {
            Syntax = new List<ISyntaxObject>
            {
                //new VariableWithStopSyntax(),
                new VariableSyntax(),
                new WritelnSyntax(),
                new WriteSyntax()
            };

            foreach (var item in Syntax)
            {
                item.Syntax.Add(new SymbolSyntax("", true, "else"));
            }
        }

        public IFTrueBlockSyntax(string context) : base(context)
        {
        }

        public IFTrueBlockSyntax(bool isNullable) : base(isNullable)
        {
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            foreach (var item in Syntax)
            {
                if (item is SyntaxObject) (item as SyntaxObject).ClearElements();
                else item.Elements.Elements.Clear();
                if (item.Check(context) == SyntaxError.NoError)
                {
                    //this.ClearElements();
                    item.Elements.Elements.RemoveAt(item.Elements.Elements.Count - 1);
                    Elements.Elements.Clear();
                    Elements.Add(item as SyntaxObject);
                    this.Context = Elements.Context;
                    return SyntaxError.NoError;
                }
            }
            return SyntaxError.SyntaxError;
        }

        public override IParserElement GetParser()
        {
            return Elements.Elements[0].ElementReference.GetParser();
        }
    }
}
