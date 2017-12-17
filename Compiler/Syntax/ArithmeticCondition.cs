using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ArithmeticCondition : AbstractSyntaxElement
    {
        public ArithmeticCondition()
        {
            Sign = new List<string>()
            {
                "=", "!=", "<", "<=", ">", ">="
            };
        }

        public ArithmeticCondition(string name) : base(name)
        {
        }

        public ArithmeticCondition(string name, params string[] signs) : base(name, signs)
        {
        }

        public override bool Check(string input)
        {
            foreach (string item in Sign)
            {
                if (item == input) return true;
            }
            return false;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Symbol;
        }
    }
}
