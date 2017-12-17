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
                    if (i == Syntax.Count - 1)
                    {
                        element = line;
                        check = Syntax[i].Check(element);
                        if (!check) return SyntaxError.SyntaxError;
                    }
                    else
                    {
                        for (int j = 1; j <= line.Length; j++)
                        {
                            element = line.Substring(0, j);
                            check = Syntax[i].Check(element);
                            if (!check)
                            {
                                element = element.Remove(element.Length - 1, 1);
                                break;
                            }

                            bool nextCheck = false;
                            if ((i + 1) < Syntax.Count)
                            {
                                if (Syntax[i + 1].Sign.Count != 0)
                                {
                                    string tmpElement = string.Empty;
                                    string nextElement = string.Empty;
                                    foreach (string item in Syntax[i + 1].Sign)
                                    {
                                        if (line != string.Empty)
                                        {
                                            if (item.Length <= line.Length)
                                            {
                                                nextElement = line.Substring(j, item.Length);
                                            }
                                        }
                                        check = Syntax[i + 1].Check(nextElement);
                                        if (check)
                                        {
                                            //if (j >= line.Length - ElementMaxPosition(i + 1) ) //- element.Length -1)
                                            {
                                                tmpElement = nextElement;
                                            }
                                        }
                                    }
                                    if (tmpElement == string.Empty) nextCheck = false;
                                    else nextCheck = true;
                                }
                            }
                            if (nextCheck) break;

                            if (j >= line.Length - (Syntax.Count - i - 1)) break;
                        }
                    }
                }
                Elements[i] = element;
                line = line.Remove(0, Elements[i].Length);
            }
            if (line != string.Empty || !IsFullSyntax()) return SyntaxError.SyntaxError;
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

        private int ElementMaxPosition(int index)
        {
            int max = 0;
            for(int i = index; i< Syntax.Count; i++)
            {
                if (Syntax[i].Sign.Count != 0)
                {
                    int maxSign = Syntax[i].Sign[0].Length;
                    foreach (string item in Syntax[i].Sign)
                    {
                        if (item.Length > maxSign) maxSign = item.Length;
                    }
                    max += maxSign;
                }
                else max += 1;
            }
            return max;
        }
    }
}
