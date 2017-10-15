using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class BracketSyntax : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public BracketSyntax()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public BracketSyntax(string name) : this()
        {
            this.Name = name;
        }

        public BracketSyntax(string name, params string[] signs) : this(name)
        {
            foreach (string sign in signs)
            {
                this.Sign.Add(sign);
            }
        }

        public bool Check(string input)
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

        public SyntaxType GetSyntaxType()
        {
            return SyntaxType.Symbol;
        }
    }
}
