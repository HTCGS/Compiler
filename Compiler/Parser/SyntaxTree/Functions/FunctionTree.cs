using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class FunctionTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; private set; }

        public FunctionTree(dynamic num)
        {
            this.Context = num;
        }

        public dynamic Execute()
        {
            foreach (var child in Childs)
            {
                child.Execute();
            }
            return null;
        }
    }
}
