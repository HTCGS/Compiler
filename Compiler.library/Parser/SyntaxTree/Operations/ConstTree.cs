using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ConstTree : AbstractSyntaxTree
    {
        public ConstTree() { }

        public ConstTree(dynamic num)
        {
            this.Context = num;
        }

        public override dynamic Execute()
        {
            return Context;
        }
    }
}