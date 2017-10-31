using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public WriteTree()
        {
            Childs = new List<ISyntaxTree>();
        }

        public WriteTree(dynamic num) : this()
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
            Console.Write(output);
            return 0;
        }
    }
}
