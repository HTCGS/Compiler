using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ElementItem
    {
        public string Element { get; set; }
        public SyntaxObject ElementReference { get; set; }
        public bool HasValue { get; set; }

        public ElementItem()
        {
            Element = string.Empty;
            HasValue = false;
        }

        public ElementItem(string element) :this()
        {
            this.Element = element;
            this.HasValue = true;
        }

        public ElementItem(SyntaxObject syntaxObject) : this()
        {
            this.ElementReference = syntaxObject;
            this.HasValue = false;
        }
    }
}
