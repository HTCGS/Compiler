using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IdSyntax : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public IdSyntax()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public IdSyntax(string name) : this()
        {
            this.Name = name;
        }

        public IdSyntax(string name, params string[] signs) :this(name)
        {
            foreach (string sign in signs)
            {
                this.Sign.Add(sign);
            }
        }

        public bool Check(string input)
        {
            VariableLex variableLex = new VariableLex();
            if (variableLex.GetKeyword(input) == Keyword.Variable) return true;
            return false;
        }

        public SyntaxType GetSyntaxType()
        {
            return SyntaxType.ID;
        }
    }
}
