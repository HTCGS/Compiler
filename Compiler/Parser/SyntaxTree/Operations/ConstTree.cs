using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ConstTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public ConstTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public ConstTree(dynamic num) : this()
        {
            this.Context = num;
        }

        public dynamic Execute()
        {
            return Context;
        }
    }
}
