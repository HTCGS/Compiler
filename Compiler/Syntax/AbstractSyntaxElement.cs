using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    abstract class AbstractSyntaxElement : ISyntaxElement
    {
        public string Name { get; set; }
        public List<string> Sign { get; set; }

        public AbstractSyntaxElement()
        {
            this.Name = "";
            this.Sign = new List<string>();
        }

        public AbstractSyntaxElement(string name) : this()
        {
            this.Name = name;
        }

        public AbstractSyntaxElement(string name, params string[] signs) :this(name)
        {
            foreach (string sign in signs)
            {
                this.Sign.Add(sign);    
            }
        }

        public abstract bool Check(string input);
        public abstract SyntaxType GetSyntaxType();
    }
}
