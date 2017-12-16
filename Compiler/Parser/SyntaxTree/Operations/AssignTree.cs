using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class AssignTree : AbstractSyntaxTree
    {
        public AssignTree()
        {
        }

        public AssignTree(string name, ISyntaxTree exp) : base(name, exp)
        {
        }

        public override dynamic Execute()
        {
            Variables.SetVariable(Context ,Childs[0].Execute());
            return 0;
        }
    }
}
