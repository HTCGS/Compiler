using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IdSyntax : AbstractSyntaxElement
    {
        public IdSyntax()
        {
        }

        public IdSyntax(string name) : base(name)
        {
        }

        public IdSyntax(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            SymbolLex symbolLex = new SymbolLex();
            foreach (char ch in input)
            {
                SymbolType symbolType = symbolLex.GetSymbolType(ch);
                if (symbolType != SymbolType.Digit
                    && symbolType != SymbolType.Letter)
                    return false;
            }
            return true;


            //VariableLex variableLex = new VariableLex();
            //if (variableLex.GetKeyword(input) == Keyword.Variable) return true;
            //return false;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.ID;
        }
    }
}
