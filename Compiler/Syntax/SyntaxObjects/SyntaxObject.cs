using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class SyntaxObject : ISyntaxObject
    {
        public List<ISyntaxElement> Syntax { get; set; }

        public string Line { get; set; }

        public string[] Elements { get; set; }

        public SyntaxObject()
        {
            this.Line = string.Empty;
        }

        public SyntaxObject(string line)
        {
            this.Line = line;
        }

        public virtual SyntaxError Check()
        {
            string line = this.Line;
            line = line.Replace(" ", string.Empty);
            line = line.Remove(0, Elements[0].Length);

            for (int i = 1; i < Syntax.Count; i++)
            {
                string element = "";
                bool check = false;
                bool lastCheck = false;
                if (Syntax[i].Sign.Count != 0)
                {
                    string tmpElement = string.Empty;
                    foreach (string item in Syntax[i].Sign)
                    {
                        if (line != string.Empty)
                        {
                            if (item.Length <= line.Length)
                            {
                                tmpElement = line.Substring(0, item.Length);
                            }
                        }
                        check = Syntax[i].Check(tmpElement);
                        if (check) element = tmpElement;
                    }
                    if (element == string.Empty) return SyntaxError.SyntaxError;
                }
                else
                {
                    if(i == Syntax.Count - 1)
                    {
                        element = line;
                        check = Syntax[i].Check(element);
                        if (!check) return SyntaxError.SyntaxError;
                    }
                    for (int j = 1; j <= line.Length; j++)
                    {
                        element = line.Substring(0, j);
                        check = Syntax[i].Check(element);
                        if (!check)
                        {
                            element = element.Remove(element.Length - 1, 1);
                        }
                        if (j == line.Length) break;
                        if (lastCheck && !check && j >= line.Length - (Syntax.Count - i - 1)) break;
                        lastCheck = check;
                    }
                    if (!check && !lastCheck) return SyntaxError.SyntaxError;
                }
                Elements[i] = element;
                line = line.Remove(0, Elements[i].Length);
            }
            if (line != string.Empty && !IsFullSyntax()) return SyntaxError.SyntaxError;
            return SyntaxError.NoError;
        }

        public virtual IParserElement GetParser()
        {
            return null;
        }

        private bool IsFullSyntax()
        {
            foreach (string item in Elements)
            {
                if (item == string.Empty) return false;
            }
            return true;
        }
    }
}
