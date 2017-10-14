using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableSyntax : ISyntaxObject
    {
        public List<ISyntaxElement> Syntax { get; set; }

        public string Line { get; private set; }
        public string[] Elements { get; private set; }

        public VariableSyntax()
        {
            Syntax = new List<ISyntaxElement>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression")
            };
            Elements = new string[Syntax.Count];
        }

        public VariableSyntax(string line) : this()
        {
            this.Line = line;
        }

        public SyntaxError Check()
        {
            string line = this.Line;
            line = line.Replace(" ", "");
            line = line.Remove(0, Elements[0].Length);
            string assign = "";
            bool check = false;
            foreach (string item in Syntax[1].Sign)
            {
                assign = line.Substring(0, item.Length);
                check = Syntax[1].Check(assign);
                if (check) break;
            }
            if (!check) return SyntaxError.SyntaxError;
            Elements[1] = assign;
            string exp = line.Remove(0, Elements[1].Length);
            if (!Syntax[2].Check(exp)) return SyntaxError.SyntaxError;
            Elements[2] = exp;
            return SyntaxError.NoError;
        }
    }
}
