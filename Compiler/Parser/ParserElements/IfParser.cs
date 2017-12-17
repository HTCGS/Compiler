using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IfParser : IParserElement
    {
        public string Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree(string text)
        {
            string[] elements = text.Split(' ');

            ExpressionParser expressionParser = new ExpressionParser(elements[4]);
            expressionParser.Normalize();
            elements[4] = expressionParser.Line;

            ISyntaxTree blockTrue = new AssignTree();
            blockTrue.Context = elements[3];
            blockTrue.Childs.Add(expressionParser.GetSyntaxTree(elements[4]));

            expressionParser.Line = elements[0];
            expressionParser.Normalize();
            elements[0] = expressionParser.Line;

            ISyntaxTree leftOp = expressionParser.GetSyntaxTree(elements[0]);

            expressionParser.Line = elements[2];
            expressionParser.Normalize();
            elements[2] = expressionParser.Line;

            ISyntaxTree rightOp = expressionParser.GetSyntaxTree(elements[2]);

            ISyntaxTree condition = GetConditionTree(elements[1]);
            condition.Childs.Add(leftOp);
            condition.Childs.Add(rightOp);

            ISyntaxTree tree = new IFTree(blockTrue, null);
            tree.Childs.Add(condition);
            return tree;
        }

        public void Normalize()
        {

        }

        private ISyntaxTree GetConditionTree(string cond)
        {
            switch (cond)
            {
                case "=": return new EqualTree();
                case "!=": return new NotEqualTree();
                case "<": return new LessTree();
                case "<=": return new LessOrEqualTree();
                case ">": return new GreaterTree();
                case ">=": return new GreaterOrEqual();
                default:
                    break;
            }
            return null;
        }
    }
}
