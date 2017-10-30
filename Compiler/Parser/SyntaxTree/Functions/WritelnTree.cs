using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public WritelnTree(dynamic num)
        {
            this.Context = num;
        }

        public dynamic Execute()
        {
            dynamic output = Variables.GetVariable(Context);
            Console.WriteLine(output);
            return 0;
        }
    }
}
