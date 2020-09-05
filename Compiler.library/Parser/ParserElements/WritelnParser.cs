using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class WritelnParser : IParserElement
    {
        public List<ElementItem> Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree()
        {
            WritelnTree tree = new WritelnTree();
            ExpressionParser expressionParser = new ExpressionParser(Line[0].Element);
            SyntaxError syntaxError = expressionParser.Check();
            if (syntaxError == SyntaxError.NoError)
            {
                expressionParser.Normalize();
                tree.Childs.Add(expressionParser.GetSyntaxTree());
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