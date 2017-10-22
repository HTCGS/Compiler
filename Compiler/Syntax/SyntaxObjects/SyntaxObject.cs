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
                    foreach (string item in Syntax[i].Sign)
                    {
                        if (line != string.Empty)
                        {
                            element = line.Substring(0, item.Length);
                        }
                        check = Syntax[i].Check(element);
                        if (check) break;
                    }
                    if (!check) return SyntaxError.SyntaxError;
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
                            break;
                        }
                        lastCheck = check;
                    }
                    if (!check && !lastCheck) return SyntaxError.SyntaxError;
                }
                Elements[i] = element;
                line = line.Remove(0, Elements[i].Length);
            }
            if (line != string.Empty) return SyntaxError.SyntaxError;
            return SyntaxError.NoError;
        }

        public virtual IParserElement GetParser()
        {
            return null;
        }
    }
}
