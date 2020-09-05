using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class AnySyntaxObject : SyntaxObject
    {
        public AnySyntaxObject()
        {
            Syntax = new List<ISyntaxObject>
            {
                new VariableSyntax(),
                new WritelnSyntax(),
                new WriteSyntax()
            };
        }

        public AnySyntaxObject(string context) : base(context) { }

        public AnySyntaxObject(bool isNullable) : base(isNullable) { }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            foreach (var item in Syntax)
            {
                if (item.Check(context) == SyntaxError.NoError)
                {
                    this.ClearElements();
                    Elements.Add(item as SyntaxObject);
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