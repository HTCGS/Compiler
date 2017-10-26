using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    interface ISyntaxTree
    {
        dynamic Context { get; set; }
        List<ISyntaxTree> Childs { get; set; }
        dynamic Execute();
    }
}
