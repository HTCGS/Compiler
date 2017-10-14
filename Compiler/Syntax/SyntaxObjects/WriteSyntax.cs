using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WriteSyntax : SyntaxObject
    {
        public WriteSyntax()
        {
            this.Syntax = new List<ISyntaxElement>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression"),
                new AssignSyntax("Assign", "#"),
                new ExpressionSyntax("Expression"),
                new IdSyntax(),
                new IdSyntax()

            };
            this.Elements = new string[Syntax.Count];
        }

        public WriteSyntax(string line) : this()
        {
            this.Line = line;
        }

    }
}
