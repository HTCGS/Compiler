using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class BracketSyntax : AbstractSyntaxElement
    {
        public BracketSyntax()
        {
        }

        public BracketSyntax(string name) : base(name)
        {
        }

        public BracketSyntax(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            if (input == "") return false;
            if (Sign.Count != 0)
            {
                foreach (char symbol in input)
                {
                    if (!Sign.Contains(symbol.ToString())) return false;
                }
            }
            else
            {
                SymbolLex symbolLex = new SymbolLex();
                foreach (char symbol in input)
                {
                    if (symbolLex.GetSymbolType(symbol) != SymbolType.Bracket) return false;
                }
            }
            return true;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Symbol;
        }
    }
}
