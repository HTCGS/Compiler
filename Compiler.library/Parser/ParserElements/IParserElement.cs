using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface IParserElement
    {
        List<ElementItem> Line { get; set; }
        SyntaxError Check();
        void Normalize();
        ISyntaxTree GetSyntaxTree();
    }
}