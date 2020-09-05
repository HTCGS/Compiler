using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SyntaxElement
    {
        public List<string> Signs { get; set; }
        public List<ElementItem> Elements { get; set; }

        public int SignCount => Signs.Count;
        public int ElementCount => Elements.Count;

        public string Context
        {
            get
            {
                string context = string.Empty;
                foreach (var item in Elements)
                {
                    if (item.HasValue)
                    {
                        context += item.Element;
                    }
                    else
                    {
                        if (item.ElementReference != null)
                        {
                            context += item.ElementReference.Elements.Context;
                        }
                    }
                }
                return context;
            }
        }

        public SyntaxElement()
        {
            Elements = new List<ElementItem>();
            Signs = new List<string>();
        }

        public SyntaxElement(string[] elements) : this()
        {
            this.Signs.AddRange(elements);
        }

        public string this [int index]
        {
            get
            {
                if (Elements.Count > index)
                {
                    if (Elements[index].HasValue)
                    {
                        return Elements[index].Element;
                    }
                    if (Elements[index].ElementReference == null)
                    {
                        return string.Empty;
                    }
                    return Elements[index].ElementReference.Context;
                }
                return null;
            }
        }

        public bool HasSign(string item)
        {
            if (Signs.Contains(item)) return true;
            return false;
        }

        public int MinElementLength()
        {
            return Signs.Min(x => x.Length);
        }

        public int MaxElementLength()
        {
            return Signs.Max(x => x.Length);
        }

        public void Add(string item)
        {
            Elements.Add(new ElementItem(item));
        }

        public void Add(SyntaxObject item)
        {
            Elements.Add(new ElementItem(item));
        }

        public void Add(ElementItem item)
        {
            Elements.Add(item);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.Signs.GetEnumerator();
        }

        public List<ElementItem> GetParserElements(params int[] index)
        {
            List<ElementItem> parserItems = new List<ElementItem>();
            foreach (int pos in index)
            {
                parserItems.Add(Elements[pos]);
            }
            return parserItems;
        }
    }
}