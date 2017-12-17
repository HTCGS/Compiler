using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ExpressionSyntax : AbstractSyntaxElement
    {
        public ExpressionSyntax()
        {
        }

        public ExpressionSyntax(string name) : base(name)
        {
        }

        public ExpressionSyntax(string name, params string[] signs) : base(name, signs)
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
                //int brackets = 0;
                SymbolLex symbolLex = new SymbolLex();
                foreach (char ch in input)
                {
                    SymbolType symbolType = symbolLex.GetSymbolType(ch);
                    if (symbolType != SymbolType.Digit
                        && symbolType != SymbolType.Letter
                        && symbolType != SymbolType.Arifmetic
                        && symbolType != SymbolType.Bracket)
                        return false;
                    //if (ch == '(') brackets++;
                    //if (ch == ')') brackets--;
                }
                //if (brackets != 0) return false;
            }
            return true;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Expression;
        }
    }
}
