using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class FunctionIdSyntax : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public FunctionIdSyntax()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public FunctionIdSyntax(string name) : this()
        {
            this.Name = name;
        }

        public FunctionIdSyntax(string name, params string[] signs) : this(name)
        {
            foreach (string sign in signs)
            {
                this.Sign.Add(sign);
            }
        }

        public bool Check(string input)
        {
            if (!Sign.Exists(str => str == input)) return false;
            return true;
        }

        public SyntaxType GetSyntaxType()
        {
            return SyntaxType.Function;
        }
    }
}
