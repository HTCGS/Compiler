using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    interface ILexerElement
    {
        string Key { get; }
        Keyword GetKeyword(string input);
    }

    enum Keyword
    {
        Unknown,
        Variable
    }
}
