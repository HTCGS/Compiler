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
            SyntaxError syntaxError = expressionParser.Check();
            if (syntaxError == SyntaxError.NoError)
            {
                expressionParser.Normalize();
                tree.Childs.Add(expressionParser.GetSyntaxTree(expressionParser.Line));
            }
            else
            {
                Console.WriteLine(syntaxError);
                return null;
            }
            return tree;
        }

        public void Normalize()
        {

        }
    }
}
