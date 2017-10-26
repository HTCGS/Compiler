using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class AssignTree : ISyntaxTree
    {
        public dynamic Context { get; set; }

        public List<ISyntaxTree> Childs { get; set; }

        public AssignTree()
        {
            this.Childs = new List<ISyntaxTree>();
        }

        public AssignTree(string name, ISyntaxTree exp) : this()
        {
            this.Context = name;
            this.Childs.Add(exp);
        }

        public dynamic Execute()
        {
            Variables.SetVariable(Context ,Childs[0].Execute());
            return 0;
        }
    }
}
