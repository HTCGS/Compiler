using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public abstract class AbstractSyntaxTree : ISyntaxTree
    {
        public dynamic Context { get; set; }
        public List<ISyntaxTree> Childs { get; set; }

        public AbstractSyntaxTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public AbstractSyntaxTree(ISyntaxTree left, ISyntaxTree right) : this()
        {
            this.Childs.Add(left);
            this.Childs.Add(right);
        }

        public AbstractSyntaxTree(string name, ISyntaxTree exp) : this()
        {
            this.Context = name;
            this.Childs.Add(exp);
        }

        public abstract dynamic Execute();
    }
}