using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class DivisionTree : AbstractSyntaxTree
    {
        public DivisionTree()
        {
        }

        public DivisionTree(ISyntaxTree left, ISyntaxTree right) : base(left, right)
        {
        }

        public override dynamic Execute()
        {
            return Childs[0].Execute() / Childs[1].Execute();
        }
    }
}
