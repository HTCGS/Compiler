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
            string[] elements = text.Split(' ');

            ISyntaxTree tree = new AssignTree();
            ExpressionParser expressionParser = new ExpressionParser(elements[1]);
            expressionParser.Normalize();
            elements[1] = expressionParser.Line;

            tree.Context = elements[0];
            tree.Childs.Add(expressionParser.GetSyntaxTree(elements[1]));
            return tree;
        }

        public void Normalize()
        {

        }
    }
}