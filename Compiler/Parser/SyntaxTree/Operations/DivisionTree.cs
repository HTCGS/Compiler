using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class DivisionTree : ISyntaxTree
    {
        public dynamic Answer { get; set; }

        public List<ISyntaxTree> Childs { get; private set; }

        public DivisionTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public DivisionTree(ISyntaxTree left, ISyntaxTree right) : this()
        {
            this.Childs.Add(left);
            this.Childs.Add(right);
        }

        public dynamic Execute()
        {
            return Childs[0].Execute() / Childs[1].Execute();
        }
    }
}
