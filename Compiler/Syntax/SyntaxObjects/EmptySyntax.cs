using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EmptySyntax : AbstractSyntaxObject
    {
        public EmptySyntax()
        {
        }

        public EmptySyntax(string context) : base(context)
        {
        }

        public override SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public override SyntaxError Check(string context)
        {
            return Check();
        }

        public override IParserElement GetParser()
        {
            return null;
        }
    }
}
