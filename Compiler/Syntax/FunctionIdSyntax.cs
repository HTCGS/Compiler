using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class FunctionIdSyntax : AbstractSyntaxObject
    {
        public FunctionIdSyntax()
        {
        }

        public FunctionIdSyntax(string context) : base(context)
        {
        }

        public FunctionIdSyntax(string context, params string[] elements) : base(context, elements)
        {
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (!Elements.HasSign(Context)) return SyntaxError.SyntaxError;
            return SyntaxError.NoError;

        }

        public override IParserElement GetParser()
        {
            return null;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Function;
        }
    }
}
