using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    interface ISyntaxObject
    {
        List<ISyntaxElement> Syntax { get; set; }
        string Line { get; set; }
        string[] Elements { get; set; }
        SyntaxError Check();
        IParserElement GetParser();
    }

    enum SyntaxError
    {
        NoError,
        SyntaxError,
        LostBracket,
        UnnownOperation,
        UndefinedVariable

    }
}
