using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class SymbolSyntax : AbstractSyntaxObject
    {
        public SymbolSyntax()
        {
        }

        public SymbolSyntax(string context) : base(context)
        {
        }

        public SymbolSyntax(string context, params string[] elements) : base(context, elements)
        {
        }

        public SymbolSyntax(string context, bool isNullable, params string[] elements) : base(context, isNullable, elements)
        {
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (Context == "") return SyntaxError.SyntaxError;
            if (Elements.Count != 0)
            {
                if (!Elements.Contains(Context)) return SyntaxError.SyntaxError;
            }
            else
            {
                SymbolLex symbolLex = new SymbolLex();
                foreach (char symbol in Context)
                {
                    if (symbolLex.GetSymbolType(symbol) != SymbolType.Letter) return SyntaxError.SyntaxError;
                }
            }
            return SyntaxError.NoError;
        }

        public override IParserElement GetParser()
        {
            return null;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Symbol;
        }
    }
}
