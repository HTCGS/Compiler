using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ExpressionSyntax : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public ExpressionSyntax()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public ExpressionSyntax(string name) : this()
        {
            this.Name = name;
        }

        public ExpressionSyntax(string name, params string[] signs) : this(name)
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
                foreach (char ch in input)
                {
                    SymbolType symbolType = symbolLex.GetSymbolType(ch);
                    if (symbolType != SymbolType.Digit
                        && symbolType != SymbolType.Letter
                        && symbolType != SymbolType.Arifmetic
                        && symbolType != SymbolType.Bracket)
                        return false;
                }
            }
            return true;
        }

        public SyntaxType GetSyntaxType()
        {
            if (this.Name == "ID") return SyntaxType.ID;
            return SyntaxType.Expression;
        }
    }
}
