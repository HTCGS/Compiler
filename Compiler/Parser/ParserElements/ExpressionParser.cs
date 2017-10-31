using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ExpressionParser : IParserElement
    {
        public string Line { get; set; }

        public ExpressionParser()
        {

        }

        public ExpressionParser(string line)
        {
            this.Line = line;
        }

        public SyntaxError Check()
        {
            SymbolLex symbolLex = new SymbolLex();
            VariableLex variableLex = new VariableLex();
            int brackets = 0;
            string var = string.Empty;
            bool arifmetic = false;
            for (int i = 0; i < Line.Length; i++)
            {
                if (Line[i] == '(') brackets++;
                if (Line[i] == ')') brackets--;

                if (symbolLex.GetSymbolType(Line[i]) != SymbolType.Arifmetic)
                {
                    if (Line[i] != '(' && Line[i] != ')') var += Line[i];
                    else
                    {
                        if (i != 0)
                        {
                            if (symbolLex.GetSymbolType(Line[i]) == SymbolType.Bracket
                                && symbolLex.GetSymbolType(Line[i - 1]) == SymbolType.Bracket)
                            {
                                if (Line[i] != Line[i - 1]) return SyntaxError.UnnownOperation;
                            }
                        }
                    }
                }
                else arifmetic = true;

                if (arifmetic)
                {
                    if (var != string.Empty)
                    {
                        if (symbolLex.GetSymbolType(var[var.Length - 1]) == SymbolType.Arifmetic)
                            return SyntaxError.UnnownOperation;

                        if (symbolLex.GetSymbolType(var[0]) == SymbolType.Digit)
                        {
                            foreach (var item in var)
                            {
                                if (symbolLex.GetSymbolType(item) != SymbolType.Digit && item != '.') return SyntaxError.UndefinedVariable;
                            }
                        }
                        else
                        {
                            if (variableLex.GetKeyword(var) != Keyword.Variable) return SyntaxError.UndefinedVariable;
                        }
                        var = string.Empty;
                    }
                    else
                    {
                        if (i == Line.Length - 1) return SyntaxError.UnnownOperation;
                        if (i != 0)
                        {
                            if (symbolLex.GetSymbolType(Line[i - 1]) == SymbolType.Arifmetic) return SyntaxError.UnnownOperation;
                        }
                        else
                        {
                            if (symbolLex.GetSymbolType(Line[i + 1]) == SymbolType.Bracket) return SyntaxError.UnnownOperation;
                        }
                    }
                    arifmetic = false;
                }
            }
            if (brackets != 0) return SyntaxError.LostBracket;
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            List<string> Operands = new List<string>();
            List<string> Functions = new List<string>();
            List<ISyntaxTree> exp = new List<ISyntaxTree>();

            SymbolLex symbolLex = new SymbolLex();
            int treeNum = 0;
            bool arifmetic = false;
            string var = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                if (symbolLex.GetSymbolType(text[i]) != SymbolType.Arifmetic
                    && symbolLex.GetSymbolType(text[i]) != SymbolType.Bracket) var += text[i];
                else arifmetic = true;

                if (arifmetic || i == text.Length - 1)
                {
                    if (var != string.Empty) Operands.Add(var);
                    if (i != text.Length - 1 || text[i] == ')') Functions.Add(text[i].ToString());
                    var = string.Empty;
                    arifmetic = false;

                    if(Functions.Count == 0 && i == text.Length -1)
                    {
                        exp.Add(OperationAnalyse(Operands, exp, 1));
                        break;
                    }
                    if (symbolLex.GetSymbolType(Functions[Functions.Count - 1][0]) != SymbolType.Bracket)
                    {
                        if (Functions.Count > 1)
                        {
                            int expNum = treeNum;
                            int valueLast = GetOperationValue(Functions[Functions.Count - 2][0]);
                            int valueNew = GetOperationValue(Functions[Functions.Count - 1][0]);
                            if (valueNew >= valueLast)
                            {
                                ISyntaxTree op = GetOperationTree(Functions[Functions.Count - 2][0]);
                                CompressOperation(Operands, exp, op, 2, ref treeNum);
                                CompressOperation(Operands, exp, op, 1, ref treeNum);

                                Operands.RemoveAt(Operands.Count - 1);
                                Operands.RemoveAt(Operands.Count - 1);
                                Functions.RemoveAt(Functions.Count - 2);
                                Operands.Add("[" + treeNum + "]");
                                for (int del = expNum; del > treeNum; del--) exp.RemoveAt(exp.Count - 1);
                                exp.Insert(treeNum, op);
                                treeNum++;
                            }
                        }
                    }
                    else
                    {
                        if (Functions[Functions.Count - 1][0] != '(')
                        {
                            Functions.RemoveAt(Functions.Count - 1);
                            ISyntaxTree tmpOp = null;
                            for (int j = Functions.Count - 1; j >= 0; j--)
                            {
                                int tmpNum = treeNum;
                                if (Functions[j][0] == '(')
                                {
                                    Functions.RemoveAt(Functions.Count - 1);
                                    break;
                                }
                                else
                                {
                                    ISyntaxTree op = GetOperationTree(Functions[Functions.Count - 1][0]);
                                    CompressOperation(Operands, exp, op, 2, ref treeNum);
                                    CompressOperation(Operands, exp, op, 1, ref treeNum);

                                    Operands.RemoveAt(Operands.Count - 1);
                                    Operands.RemoveAt(Operands.Count - 1);
                                    Functions.RemoveAt(Functions.Count - 1);
                                    Operands.Add("[" + treeNum + "]");
                                    exp.Insert(treeNum, op);
                                    treeNum++;
                                    if (tmpOp != null) op.Childs.Add(tmpOp);
                                    tmpOp = op;
                                }
                            }
                        }
                    }
                }
            }

            if (Functions.Count == 0) return exp[0];
            ISyntaxTree lastOp = GetOperationTree(Functions[Functions.Count - 1][0]);
            for (int i = Functions.Count - 1; i >= 0; i--)
            {
                ISyntaxTree operation = GetOperationTree(Functions[i][0]);
                if (i == Functions.Count - 1)
                {
                    operation.Childs.Add(OperationAnalyse(Operands, exp, 2));
                    operation.Childs.Add(OperationAnalyse(Operands, exp, 1));
                    Operands.RemoveAt(Operands.Count - 1);
                    Operands.RemoveAt(Operands.Count - 1);
                    lastOp = operation;
                }
                else
                {
                    operation.Childs.Add(OperationAnalyse(Operands, exp, 1));
                    Operands.RemoveAt(Operands.Count - 1);
                    Functions.RemoveAt(Functions.Count - 1);
                    operation.Childs.Add(lastOp);
                    lastOp = operation;
                }
            }
            return lastOp;
        }

        public void Normalize()
        {
            SymbolLex symbolLex = new SymbolLex();
            bool isMinus = false;
            int start = 0;
            int last = 3;
            string var = string.Empty;
            for (int i = 0; i < Line.Length; i++)
            {
                if (isMinus)
                {
                    var += Line[i];
                }
                if (symbolLex.GetSymbolType(Line[i]) == SymbolType.Arifmetic
                    || symbolLex.GetSymbolType(Line[i]) == SymbolType.Bracket
                    || (i + 1 == Line.Length))
                {
                    if (isMinus)
                    {
                        isMinus = false;
                        if (i != Line.Length - 1 || var[var.Length - 1] == ')') var = var.Remove(var.Length - 1, 1);
                        last = var.Length + 1;
                        if (i - last > 0) last++;
                        if (i - last < 0) last--;

                        string head = Line.Substring(0, start);
                        string tail = Line.Substring(i, Line.Length - (head.Length + var.Length) - 1);
                        if (Line[i - last] != '(' && symbolLex.GetSymbolType(Line[i - last]) != SymbolType.Arifmetic)
                        {
                            var = "+(0-" + var + ")";
                            i += 4;
                        }
                        else
                        {
                            if ((start != 0 && i != Line.Length - 1) || (start != 0 && i == Line.Length - 1)
                                && Line[start - 1] == '(' && Line[i] == ')')
                            {
                                var = "0-" + var;
                                i += 1;
                            }
                            else
                            {
                                var = "(0-" + var + ")";
                                i += 3;
                            }
                        }
                        Line = head + var + tail;
                        var = string.Empty;
                        last = 3;
                    }
                    if (Line[i] == '-' && i != Line.Length - 1)
                    {
                        if (symbolLex.GetSymbolType(Line[i + 1]) != SymbolType.Bracket)
                        {
                            isMinus = true;
                            start = i;
                        }
                    }
                }
            }
        }

        private int GetOperationValue(char operation)
        {
            switch (operation)
            {
                case '-': return 2;
                case '+': return 2;
                case '*': return 1;
                case '/': return 1;
                case '(': return 3;
                default:
                    break;
            }
            return 0;
        }

        private ISyntaxTree GetOperationTree(char operation)
        {
            switch (operation)
            {
                case '-': return new SubtractionTree();
                case '+': return new AddTree();
                case '*': return new MultiplicationTree();
                case '/': return new DivisionTree();
                default:
                    break;
            }
            return null;
        }

        private int GetExpressionNum(string exp)
        {
            string num = string.Empty;
            for (int i = 1; i < exp.Length - 1; i++)
            {
                num += exp[i];
            }
            return Convert.ToInt32(num);
        }

        private ISyntaxTree OperationAnalyse(List<string> Operands, List<ISyntaxTree> exp, int index)
        {
            SymbolLex symbolLex = new SymbolLex();
            if (symbolLex.GetSymbolType(Operands[Operands.Count - index][0]) == SymbolType.Digit)
            {
                return new ConstTree(Convert.ToInt32(Operands[Operands.Count - index]));
            }
            else if (symbolLex.GetSymbolType(Operands[Operands.Count - index][0]) == SymbolType.SquareBracket)
            {
                int expNum = GetExpressionNum(Operands[Operands.Count - index]);
                return exp[expNum];
            }
            else return new VariableTree(Operands[Operands.Count - index]);
        }

        private ISyntaxTree CompressOperation(List<string> Operands, List<ISyntaxTree> exp, ISyntaxTree op, int index, ref int treeNum)
        {
            int expNum;
            SymbolLex symbolLex = new SymbolLex();
            if (symbolLex.GetSymbolType(Operands[Operands.Count - index][0]) == SymbolType.Digit)
            {
                op.Childs.Add(new ConstTree(Convert.ToInt32(Operands[Operands.Count - index])));
            }
            else if (symbolLex.GetSymbolType(Operands[Operands.Count - index][0]) == SymbolType.SquareBracket)
            {
                if (index == 1)
                {
                    expNum = GetExpressionNum(Operands[Operands.Count - index]);
                    op.Childs.Add(exp[expNum]);
                    treeNum--;
                }
                else
                {
                    int expNum2 = GetExpressionNum(Operands[Operands.Count - index]);
                    op.Childs.Add(exp[expNum2]);
                    treeNum--;
                }
            }
            else op.Childs.Add(new VariableTree(Operands[Operands.Count - index]));
            return op;
        }
    }
}
