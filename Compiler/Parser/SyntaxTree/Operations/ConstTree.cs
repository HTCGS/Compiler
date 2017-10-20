using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ConstTree : ISyntaxTree
    {
        public dynamic Answer { get; set; }

        public List<ISyntaxTree> Childs { get; private set; }

        public ConstTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public ConstTree(dynamic num) : this()
        {
            this.Answer = num;
        }

        public dynamic Execute()
        {
            return Answer;
        }
    }
}
