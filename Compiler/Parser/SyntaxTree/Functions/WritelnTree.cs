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

        public WritelnTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public WritelnTree(dynamic num) :this()
        {
            this.Context = num;
        }

        public dynamic Execute()
        {
            dynamic output = 0;
            if (Childs.Count == 0)
            {
                output = Variables.GetVariable(Context);
            }
            else
            {
                output = Childs[0].Execute();
            }
            Console.WriteLine(output);
            return 0;
        }
    }
}
