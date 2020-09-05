using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class DivisionTree : AbstractSyntaxTree
    {
        public DivisionTree() { }

        public DivisionTree(ISyntaxTree left, ISyntaxTree right) : base(left, right) { }

        public override dynamic Execute()
        {
            if (Childs[1].Execute() == 0)
            {
                Console.WriteLine("Divide by zero!");
                return 0;
            }
            return Childs[0].Execute() / Childs[1].Execute();
        }
    }
}