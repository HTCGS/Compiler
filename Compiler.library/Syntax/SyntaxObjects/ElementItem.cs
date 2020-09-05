using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ElementItem
    {
        public string Element { get; set; }
        public SyntaxObject ElementReference { get; set; }
        public bool HasValue { get; set; }

        public ElementItem()
        {
            Element = string.Empty;
            HasValue = false;
        }

        public ElementItem(string element) : this()
        {
            this.Element = element;
            if (element != "") this.HasValue = true;
            else this.HasValue = false;
        }

        public ElementItem(SyntaxObject syntaxObject) : this()
        {
            this.ElementReference = syntaxObject;
            this.HasValue = false;
        }
    }
}