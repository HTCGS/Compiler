using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    interface ISyntaxObject
    {
        List<ISyntaxElement> Syntax { get; }
        string Line { get; }
        string[] Elements { get; }
        SyntaxError Check();
    }

    enum SyntaxError
    {
        NoError,
        SyntaxError

    }
}
