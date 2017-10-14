using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    interface ISyntaxElement
    {
        string Name { get; set; }
        List<string> Sign { get; set; }
        bool Check(string input);
        SyntaxType GetSyntaxType();
    }

    enum SyntaxType
    {
        keyword,
        ID,
        Expression,
        Assign
    }
}
