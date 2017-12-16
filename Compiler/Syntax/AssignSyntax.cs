using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class AssignSyntax : AbstractSyntaxElement
    {
        public AssignSyntax()
        {
        }

        public AssignSyntax(string name) : base(name)
        {
        }

        public AssignSyntax(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            if (!Sign.Exists(s => s == input)) return false;
            return true;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Assign;
        }
    }
}
