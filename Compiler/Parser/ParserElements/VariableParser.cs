using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableParser : IParserElement
    {
        public string Line { get; set; }

        public SyntaxError Check()
        {
            int spacePos = Line.IndexOf(" ");
            string exp = Line.Substring(spacePos + 1, Line.Length - spacePos - 1);

            ExpressionParser expressionParser = new ExpressionParser(exp);
            return expressionParser.Check();
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            int spacePos = Line.IndexOf(" ");
            string var = Line.Substring(0, spacePos);
            string exp = Line.Substring(spacePos + 1, Line.Length - spacePos - 1);

            ISyntaxTree tree = new AssignTree();
            ExpressionParser expressionParser = new ExpressionParser(exp);
            expressionParser.Normalize();
            exp = expressionParser.Line;

            tree.Context = var;
            tree.Childs.Add(expressionParser.GetSyntaxTree(exp));
            return tree;
        }

        public void Normalize()
        {

        }
    }
}