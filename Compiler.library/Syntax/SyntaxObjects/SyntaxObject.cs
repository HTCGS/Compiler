using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SyntaxObject : AbstractSyntaxObject
    {
        private int lastNullableIndex = -1; // if -1 syntax doesnt have nullables

        public SyntaxObject() { }

        public SyntaxObject(string context) : base(context) { }

        public SyntaxObject(bool isNullable) : base(isNullable) { }

        public override SyntaxError Check()
        {
            string line = this.Context;
            line = line.Replace(" ", string.Empty);
            int startIndex = 0;
            if (Elements.ElementCount != 0)
            {
                line = line.Remove(0, Elements[0].Length);
                startIndex = 1;
            }
            else startIndex = 0;

            int i = 0;
            SyntaxError check = SyntaxError.SyntaxError;
            for (i = startIndex; i < Syntax.Count; i++)
            {
                string element = "";
                if (Syntax[i] is SyntaxObject)
                {
                    check = Syntax[i].Check(line);
                    if (check != SyntaxError.NoError) break;
                }
                else
                {
                    if (Syntax[i].Elements.SignCount != 0)
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
                        if (element == string.Empty) break;
                    }
                    else
                    {
                        //if (i == Syntax.Count - 1)
                        //{
                        //    element = line;
                        //    check = Syntax[i].Check(element);
                        //    if (check != SyntaxError.NoError) break;
                        //}
                        //else
                        {
                            bool isFound = false;
                            for (int j = 1; j <= line.Length; j++)
                            {
                                element = line.Substring(0, j);
                                check = Syntax[i].Check(element);
                                if (check != SyntaxError.NoError)
                                {
                                    element = element.Remove(element.Length - 1, 1);
                                    break;
                                }

                                if ((i + 1) < Syntax.Count)
                                {
                                    if (Syntax[i + 1] is SyntaxObject)
                                    {
                                        string nextLine = line.Substring(element.Length, line.Length - element.Length);
                                        if (Syntax[i + 1].Check(nextLine) != SyntaxError.NoError) isFound = false;
                                        else isFound = true;
                                        (Syntax[i + 1] as SyntaxObject).ClearElements();
                                    }
                                    else
                                    {
                                        if (Syntax[i + 1].Elements.SignCount != 0)
                                        {
                                            isFound = CheckNext(line, j, i + 1, element);
                                        }
                                    }
                                }
                                if (isFound) break;
                                if (j >= line.Length - (Syntax.Count - i - 1)) break;
                            }
                            if (i != Syntax.Count - 1)
                            {
                                if (!isFound)
                                {
                                    check = SyntaxError.SyntaxError;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (Syntax[i] is SyntaxObject) Elements.Add(Syntax[i] as SyntaxObject);
                else
                {
                    if (Syntax[i] is AbstractSyntaxObject) Elements.Add(element);
                }
                line = line.Remove(0, Elements[Elements.ElementCount - 1].Length);
            }

            if (check != SyntaxError.NoError)
            {
                for (int n = 0; n < Syntax.Count; n++)
                {
                    SyntaxObject syntaxObject = Activator.CreateInstance(this.GetType()) as SyntaxObject;
                    syntaxObject.Context = this.Context;
                    syntaxObject.Syntax.Clear();
                    syntaxObject.Syntax.AddRange(this.Syntax);
                    int nullablePos = GetNextNullableElement();
                    if (nullablePos != -1)
                    {
                        syntaxObject.Syntax.RemoveAt(nullablePos);
                        check = syntaxObject.Check();
                        if (check == SyntaxError.NoError)
                        {
                            AddNewSyntax(syntaxObject);
                            line = string.Empty;
                            break;
                        }
                        //else return check;
                    }
                    else return check;
                }
            }

            this.Context = Elements.Context;

            //if (line != string.Empty || !IsFulSyntax()) return SyntaxError.SyntaxError;
            if (!IsFulSyntax()) return SyntaxError.SyntaxError;
            return SyntaxError.NoError;
        }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        private bool IsFulSyntax()
        {
            if (Elements.ElementCount != Syntax.Count) return false;
            for (int i = 0; i < Elements.Elements.Count; i++)
            {
                if (!Elements.Elements[i].HasValue &&
                    Elements.Elements[i].ElementReference == null)
                {
                    if (!Syntax[i].IsNullable) return false;
                }
            }
            //if (Syntax[Elements.ElementCount].IsNullable) return true;
            return true;
        }

        private bool CheckNext(string line, int pos, int index, string element)
        {
            SyntaxError nextCheck = SyntaxError.SyntaxError;
            string tmpElement = string.Empty;
            string nextElement = string.Empty;
            foreach (string item in Syntax[index].Elements)
            {
                if (line != string.Empty)
                {
                    if (item.Length <= line.Length && pos + item.Length <= line.Length)
                    {
                        nextElement = line.Substring(pos, item.Length);
                    }
                }
                nextCheck = Syntax[index].Check(nextElement);
                if (nextCheck == SyntaxError.NoError)
                {
                    int from = element.Length + ElementMinPosition(index - 1, index) - 1;
                    int to = line.Length - ElementMaxPosition(index);
                    if (to != line.Length - 1) from--;

                    if (pos > from && pos <= to)
                    {
                        tmpElement = nextElement;
                    }
                    if (from == to && pos == from) tmpElement = nextElement;
                }
            }
            if (tmpElement == string.Empty) return false;
            else return true;
        }

        private int ElementMinPosition(int start, int index)
        {
            int min = 0;
            for (int i = start; i < index; i++)
            {
                if (Syntax[i].Elements.SignCount != 0)
                {
                    min += Syntax[i].Elements.MinElementLength();
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
                if (Syntax[i].Elements.SignCount != 0)
                {
                    max += Syntax[i].Elements.MinElementLength();
                }
                else max += 1;
            }
            return max;
        }

        private void AddNewSyntax(SyntaxObject newObject)
        {
            int newPos = 0;
            this.Elements.Elements.Clear();
            for (int i = 0; i < Syntax.Count; i++)
            {
                if (this.Syntax[i].GetType() == newObject.Syntax[newPos].GetType())
                {
                    Elements.Add(newObject.Elements.Elements[newPos]);
                    if (newObject.Syntax.Count > newPos + 1) newPos++;
                    else
                    {
                        for (int j = i + 1; j < Syntax.Count; j++)
                        {
                            Elements.Add("");
                        }
                        return;
                    }
                }
                else
                {
                    Elements.Add("");
                }
            }
        }

        private int GetNextNullableElement()
        {
            int start = 0;
            if (lastNullableIndex != -1)
            {
                start = lastNullableIndex + 1;
            }
            for (int i = start; i < Syntax.Count; i++)
            {
                if (Syntax[i].IsNullable)
                {
                    lastNullableIndex = i;
                    return lastNullableIndex;
                }
            }
            lastNullableIndex = -1;
            return lastNullableIndex;
        }

        public void ClearElements()
        {
            foreach (var item in Elements.Elements)
            {
                if (!item.HasValue)
                {
                    if (item.ElementReference != null)
                    {
                        item.ElementReference.ClearElements();
                    }
                }
            }
            Elements.Elements.Clear();
            lastNullableIndex = -1;
        }
    }
}