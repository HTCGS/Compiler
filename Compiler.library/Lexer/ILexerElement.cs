using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface ILexerElement
    {
        string Key { get; set; }
        Keyword GetKeyword(string input);
        ISyntaxObject GetSyntaxScaner();
    }

    public enum Keyword
    {
        Unknown,
        LanguageSymbols,
        Key,
        Variable,
        Function,
        Comment
    }
}