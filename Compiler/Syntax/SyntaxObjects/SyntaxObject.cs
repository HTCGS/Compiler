using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class SyntaxObject : AbstractSyntaxObject
    {
        public SyntaxObject()
        {
        }

        public SyntaxObject(string context) : base(context)
        {
        }

        public override SyntaxError Check()
        {
            string line = this.Context;
            line = line.Replace(" ", string.Empty);
            line = line.Remove(0, Elements[0].Length);

            for (int i = 1; i < Syntax.Count; i++)
            {
                if (line == "" && Syntax[i].IsNullable) break;
                string element = "";
                SyntaxError check = SyntaxError.SyntaxError;
                if (Syntax[i].Elements.Count != 0)
                {
                    string tmpElement = string.Empty;
                    foreach (string item in Syntax[i].Elements)
                    {
                        if (line != string.Empty)
                        {
                            if (item.Length <= line.Length)
                            {
                                tmpElement = line.Substring(0, item.Length);
                            }
                        }
                        check = Syntax[i].Check(tmpElement);
                        if (check == SyntaxError.NoError) element = tmpElement;
                    }
                    if (element == string.Empty) return check;
                }
                else
                {
                    if (i == Syntax.Count - 1)
                    {
                        element = line;
                        check = Syntax[i].Check(element);
                        if (check != SyntaxError.NoError) return check;
                    }
                    else
                    {
                        for (int j = 1; j <= line.Length; j++)
                        {
                            element = line.Substring(0, j);
                            check = Syntax[i].Check(element);
                            if (check != SyntaxError.NoError)
                            {
                                element = element.Remove(element.Length - 1, 1);
                                break;
                            }

                            bool nextCheck = false;
                            if ((i + 1) < Syntax.Count)
                            {
                                if (Syntax[i + 1].Elements.Count != 0)
                                {
                                    string tmpElement = string.Empty;
                                    string nextElement = string.Empty;
                                    foreach (string item in Syntax[i + 1].Elements)
                                    {
                                        if (line != string.Empty)
                                        {
                                            if (item.Length <= line.Length && j + item.Length < line.Length)
                                            {
                                                nextElement = line.Substring(j, item.Length);
                                            }
                                        }
                                        check = Syntax[i + 1].Check(nextElement);
                                        if (check == SyntaxError.NoError)
                                        {
                                            int from = element.Length + ElementMinPosition(i, i + 1) - 1;
                                            int to = line.Length - ElementMaxPosition(i + 1);
                                            if (to != line.Length - 1) from--;
                                            if (j > from && j <= to)
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
                Elements.Add(element);
                line = line.Remove(0, Elements[Elements.Count - 1].Length);
            }
            if (line != string.Empty || !IsFulSyntax()) return SyntaxError.SyntaxError;
            return SyntaxError.NoError;
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override IParserElement GetParser()
        {
            return null;
        }

        private bool IsFulSyntax()
        {
            if (Elements.Count == Syntax.Count) return true;
            if (Syntax[Elements.Count].IsNullable) return true;
            return false;
        }

        private int ElementMinPosition(int start, int index)
        {
            int min = 0;
            for (int i = start; i < index; i++)
            {
                if (Syntax[i].Elements.Count != 0)
                {
                    int minSign = Syntax[i].Elements[0].Length;
                    foreach (string item in Syntax[i].Elements)
                    {
                        if (item.Length < minSign) minSign = item.Length;
                    }
                    min += minSign;
                }
                else min += 1;
            }
            return min;
        }

        private int ElementMaxPosition(int index)
        {
            int max = 0;
            for (int i = index; i < Syntax.Count; i++)
            {
                if (Syntax[i].Elements.Count != 0)
                {
                    int maxSign = Syntax[i].Elements[0].Length;
                    foreach (string item in Syntax[i].Elements)
                    {
                        if (item.Length < maxSign) maxSign = item.Length;
                    }
                    max += maxSign;
                }
                else max += 1;
            }
            return max;
        }
    }
}
