using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class IdSyntax : AbstractSyntaxObject
    {
        public IdSyntax() { }

        public IdSyntax(string context) : base(context) { }

        public IdSyntax(string context, params string[] elements) : base(context, elements) { }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            SymbolLex symbolLex = new SymbolLex();
            foreach (char ch in Context)
            {
                SymbolType symbolType = symbolLex.GetSymbolType(ch);
                if (symbolType != SymbolType.Digit &&
                    symbolType != SymbolType.Letter)
                    return SyntaxError.UnknownID;
            }
            return SyntaxError.NoError;
        }

        public override IParserElement GetParser()
        {
            return null;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.ID;
        }
    }
}