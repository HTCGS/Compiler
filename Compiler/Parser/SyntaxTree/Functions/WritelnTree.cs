using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnTree : AbstractSyntaxTree
    {
        public WritelnTree()
        {
        }

        public WritelnTree(dynamic num)
        {
            this.Context = num;
        }

        public override dynamic Execute()
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
