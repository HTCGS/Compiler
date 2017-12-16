using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class SymbolSyntax : AbstractSyntaxElement
    {
        public SymbolSyntax()
        {
        }

        public SymbolSyntax(string name) : base(name)
        {
        }

        public SymbolSyntax(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            if (input == "") return false;
            if (Sign.Count != 0)
            {
                if (!Sign.Contains(input)) return false;
            }
            else
            {
                SymbolLex symbolLex = new SymbolLex();
                foreach (char symbol in input)
                {
                    if (symbolLex.GetSymbolType(symbol) != SymbolType.Letter) return false;
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
