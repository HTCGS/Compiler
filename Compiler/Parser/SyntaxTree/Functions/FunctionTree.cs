using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class FunctionTree : AbstractSyntaxTree
    {
        public FunctionTree()
        {
        }

        public FunctionTree(dynamic num)
        {
            this.Context = num;
        }

        public override dynamic Execute()
        {
            foreach (var child in Childs)
            {
                child.Execute();
            }
            return null;
        }
    }
}
