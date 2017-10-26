using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public VariableTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public VariableTree(dynamic num) : this()
        {
            this.Context = num;
        }

        public dynamic Execute()
        {
            return Variables.GetVariable(Context);
        }
    }
}
