using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EqualsTree : ISyntaxTree
    {
         public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public EqualsTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public EqualsTree(ISyntaxTree left, ISyntaxTree right) : this()
        {
            this.Childs.Add(left);
            this.Childs.Add(right);
        }

        public dynamic Execute()
        {
            //bool equals = false;
            //if (Childs[0].Execute() == Childs[1].Execute()) equals = true;
            //return equals;

            return Childs[0].Execute() == Childs[1].Execute();
        }
    }
}
