using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class MultiplicationTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public MultiplicationTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public MultiplicationTree(ISyntaxTree left, ISyntaxTree right) : this()
        {
            this.Childs.Add(left);
            this.Childs.Add(right);
        }

        public dynamic Execute()
        {
            return Childs[0].Execute() * Childs[1].Execute();
        }
    }
}
