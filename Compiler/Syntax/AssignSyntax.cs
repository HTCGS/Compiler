using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class AssignSyntax : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public AssignSyntax()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public AssignSyntax(string name) :this()
        {
            this.Name = name;
        }

        public AssignSyntax(string name, params string[] signs) : this(name)
        {
            foreach (string sign in signs)
            {
                this.Sign.Add(sign);
            }
        }

        public bool Check(string input)
        {
            if (!Sign.Exists(s => s == input)) return false;
            return true;
        }

        public SyntaxType GetSyntaxType()
        {
            return SyntaxType.Assign;
        }
    }
}
