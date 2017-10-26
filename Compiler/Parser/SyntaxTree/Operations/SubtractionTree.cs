using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class SubtractionTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public SubtractionTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public SubtractionTree(ISyntaxTree left, ISyntaxTree right) : this()
        {
            this.Childs.Add(left);
            this.Childs.Add(right);
        }

        public dynamic Execute()
        {
            return Childs[0].Execute() - Childs[1].Execute();
        }
    }
}
