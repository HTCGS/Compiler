using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EqualsTree : AbstractSyntaxTree
    {
        public EqualsTree()
        {
        }

        public EqualsTree(ISyntaxTree left, ISyntaxTree right) : base(left, right)
        {
        }

        public override dynamic Execute()
        {
            //bool equals = false;
            //if (Childs[0].Execute() == Childs[1].Execute()) equals = true;
            //return equals;

            return Childs[0].Execute() == Childs[1].Execute();
        }
    }
}
