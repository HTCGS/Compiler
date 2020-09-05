using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ArithmeticConditionSymbol : AbstractSyntaxObject
    {
        public ArithmeticConditionSymbol()
        {
            Elements = new SyntaxElement()
            {
                Signs = new List<string>()
                {
                "=",
                "<",
                ">",
                "!=",
                "<=",
                ">="
                }
            };
        }

        public ArithmeticConditionSymbol(string context) : base(context) { }

        public ArithmeticConditionSymbol(string context, params string[] elements) : base(context, elements) { }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (Elements.HasSign(Context)) return SyntaxError.NoError;
            return SyntaxError.LostOperation;
        }

        public override IParserElement GetParser()
        {
            return null;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.ArithmeticSymbol;
        }
    }
}