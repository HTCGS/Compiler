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

        public ExpressionParser(string line)
        {
            this.Line = line;
        }

        public SyntaxError Check()
        {
            SymbolLex symbolLex = new SymbolLex();
            VariableLex variableLex = new VariableLex();
            int brackets = 0;
            int start = 0;
            string var = string.Empty;
            bool arifmetic = false;
            for (int i = 0; i < Line.Length; i++)
            {
                if (Line[i] == '(') brackets++;
                if (Line[i] == ')') brackets--;
                if (arifmetic)
                {
                    if (Line[i] != '(' && Line[i] != ')') var += Line[i];
                }
                if (symbolLex.GetSymbolType(Line[i]) == SymbolType.Arifmetic || i == Line.Length -1)
                {
                    if (arifmetic && i == start + 1) return SyntaxError.UnnownOperation;
                    if (!arifmetic)
                    {
                        arifmetic = true;
                        start = i;
                    }
                    else
                    {
                        var = var.Remove(var.Length - 1, 1);
                        if (var != string.Empty)
                        {
                            if (symbolLex.GetSymbolType(var[0]) == SymbolType.Digit)
                            {
                                foreach (var item in var)
                                {
                                    if (symbolLex.GetSymbolType(item) != SymbolType.Digit) return SyntaxError.UndefinedVariable;
                                }
                            }
                            else
                            {
                                if (variableLex.GetKeyword(var) != Keyword.Variable) return SyntaxError.UndefinedVariable;
                            }
                        }
                        var = string.Empty;
                        start = i;
                    }
                }
            }
            if (brackets != 0) return SyntaxError.LostBracket;
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTreeOperation()
        {
            throw new NotImplementedException();
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
                        if (i != Line.Length - 1) var = var.Remove(var.Length - 1, 1);
                        last = var.Length + 1;
                        if (i - last > 0) last++;
                        if (i - last < 0) last--;

                        string head = Line.Substring(0, start);
                        string tail = Line.Substring(i, Line.Length - (head.Length + var.Length) - 1);
                        if (Line[i - last] != '(' && symbolLex.GetSymbolType(Line[i - last]) != SymbolType.Arifmetic)
                        {
                            var = "+(-1*" + var + ")";

                            i += 5;
                        }
                        else
                        {
                            var = "(-1*" + var + ")";
                            i += 4;
                        }
                        Line = head + var + tail;
                        var = string.Empty;
                        last = 3;
                    }
                    if (Line[i] == '-')
                    {
                        if (symbolLex.GetSymbolType(Line[i + 1]) == SymbolType.Letter
                            || symbolLex.GetSymbolType(Line[i + 1]) != SymbolType.Bracket)
                        {
                            isMinus = true;
                            start = i;
                        }
                    }
                }
            }
        }
    }
}
