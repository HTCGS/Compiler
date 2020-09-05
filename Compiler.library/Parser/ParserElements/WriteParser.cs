using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class WriteParser : IParserElement
    {
        public List<ElementItem> Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree()
        {
            WriteTree tree = new WriteTree();
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