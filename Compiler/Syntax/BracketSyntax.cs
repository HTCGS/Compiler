using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class BracketSyntax : AbstractSyntaxObject
    {
        public BracketSyntax()
        {
        }

        public BracketSyntax(string context) : base(context)
        {
        }

        public BracketSyntax(string context, params string[] elements) : base(context, elements)
        {
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (Context == "") return SyntaxError.LostBracket;
            if (Elements.Count != 0)
            {
                foreach (char symbol in Context)
                {
                    if (!Elements.Contains(symbol.ToString())) return SyntaxError.LostBracket;
                }
            }
            else
            {
                SymbolLex symbolLex = new SymbolLex();
                foreach (char symbol in Context)
                {
                    if (symbolLex.GetSymbolType(symbol) != SymbolType.Bracket) return SyntaxError.LostBracket;
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
            return SyntaxType.Bracket;
        }
    }
}
