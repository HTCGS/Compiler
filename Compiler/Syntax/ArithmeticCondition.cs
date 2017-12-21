using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ArithmeticCondition : AbstractSyntaxObject
    {
        public ArithmeticCondition()
        {
            Elements = new List<string>()
            {
                "=", "!=", "<", "<=", ">", ">="
            };
        }

        public ArithmeticCondition(string context) : base(context)
        {
        }

        public ArithmeticCondition(string context, params string[] elements) : base(context, elements)
        {
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            foreach (string item in Elements)
            {
                if (item == Context) return SyntaxError.NoError;
            }
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
