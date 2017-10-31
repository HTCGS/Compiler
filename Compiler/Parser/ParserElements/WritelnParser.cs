using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class WritelnParser : IParserElement
    {
        public string Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            WritelnTree tree = new WritelnTree();
            ExpressionParser expressionParser = new ExpressionParser(text);
            if (expressionParser.Check() == SyntaxError.NoError)
            {
                expressionParser.Normalize();
                tree.Childs.Add(expressionParser.GetSyntaxTree(expressionParser.Line));
            }
            else return null;
            //else tree.Context = text;
            return tree;
        }

        public void Normalize()
        {

        }
    }
}
