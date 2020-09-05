using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class VariableParser : IParserElement
    {
        public List<ElementItem> Line { get; set; }

        public SyntaxError Check()
        {
            ExpressionParser expressionParser = new ExpressionParser(Line[1].Element);
            return expressionParser.Check();
        }

        public ISyntaxTree GetSyntaxTree()
        {
            ISyntaxTree tree = new AssignTree();
            ExpressionParser expressionParser = new ExpressionParser(Line[1].Element);
            expressionParser.Normalize();
            Line[1].Element = expressionParser.Line[0].Element;

            tree.Context = Line[0].Element;
            tree.Childs.Add(expressionParser.GetSyntaxTree());
            return tree;
        }

        public void Normalize()
        {

        }
    }
}