using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IFTree : AbstractSyntaxTree
    {
        public IFTree()
        {
        }

        public IFTree(ISyntaxTree left, ISyntaxTree right) : base(left, right)
        {
        }

        public override dynamic Execute() //true    false
        {
            if (Childs[2].Execute())
            {
                Childs[0].Execute();
            }
            else
            {
                if (Childs[1] != null)
                {
                    Childs[1].Execute();
                }
            }
            return 0;
        }
    }
}
