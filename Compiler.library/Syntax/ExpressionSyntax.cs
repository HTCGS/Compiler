using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ExpressionSyntax : AbstractSyntaxObject
    {
        public ExpressionSyntax() { }

        public ExpressionSyntax(string context) : base(context) { }

        public ExpressionSyntax(bool isNullable) : base(isNullable) { }

        public ExpressionSyntax(string context, params string[] elements) : base(context, elements) { }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (Context == "") return SyntaxError.SyntaxError;
            if (Elements.SignCount != 0)
            {
                foreach (char symbol in Context)
                {
                    if (!Elements.HasSign(symbol.ToString())) return SyntaxError.SyntaxError;
                }
            }
            else
            {
                SymbolLex symbolLex = new SymbolLex();
                foreach (char ch in Context)
                {
                    SymbolType symbolType = symbolLex.GetSymbolType(ch);
                    if (symbolType != SymbolType.Digit &&
                        symbolType != SymbolType.Letter &&
                        symbolType != SymbolType.Arifmetic &&
                        symbolType != SymbolType.Bracket)
                        return SyntaxError.SyntaxError;
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
            return SyntaxType.Expression;
        }
    }
}