using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class GreaterOrEqual : AbstractSyntaxTree
    {
        public GreaterOrEqual() { }

        public GreaterOrEqual(ISyntaxTree left, ISyntaxTree right) : base(left, right) { }

        public override dynamic Execute()
        {
            return Childs[0].Execute() >= Childs[1].Execute();
        }
    }
}