using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class FunctionIdSyntax : AbstractSyntaxElement
    {
        public FunctionIdSyntax()
        {
        }

        public FunctionIdSyntax(string name) : base(name)
        {
        }

        public FunctionIdSyntax(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            if (!Sign.Exists(str => str == input)) return false;
            return true;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Function;
        }
    }
}
