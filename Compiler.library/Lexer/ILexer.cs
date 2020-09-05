using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface ILexer
    {
        List<ILexerElement> LexElements { get; set; }
        string[] ReadFile(string path);
        ISyntaxObject ScanFile();
    }
}