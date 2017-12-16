using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableTree : AbstractSyntaxTree
    {
        public VariableTree()
        {
        }

        public VariableTree(dynamic num) 
        {
            this.Context = num;
        }

        public override dynamic Execute()
        {
            return Variables.GetVariable(Context);
        }
    }
}
