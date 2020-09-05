using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface IParser
    {
        List<IParserElement> ParserElements { get; set; }
        List<ISyntaxObject> Syntax { get; set; }
        SyntaxError Check();
    }
}