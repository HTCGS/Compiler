using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class IfParser : IParserElement
    {
        public List<ElementItem> Line { get; set; }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }

        public ISyntaxTree GetSyntaxTree()
        {
            ExpressionParser expressionParser = new ExpressionParser(Line[4]);
            expressionParser.Normalize();
            Line[4].Element = expressionParser.Line[0].Element;

            //ISyntaxTree blockTrue = new AssignTree();
            //blockTrue.Context = Line[3].Element;
            //blockTrue.Childs.Add(expressionParser.GetSyntaxTree());
            ISyntaxTree blockTrue = Line[3].ElementReference.GetParser().GetSyntaxTree();

            //ISyntaxTree blockFalse = new AssignTree();
            //if (Line.Count > 5)
            //{
            //    expressionParser = new ExpressionParser(Line[6]);
            //    expressionParser.Normalize();
            //    Line[6] = expressionParser.Line[0];

            //    blockFalse.Context = Line[5];
            //    blockFalse.Childs.Add(expressionParser.GetSyntaxTree());
            //}
            //else blockFalse = null;

            ISyntaxTree blockFalse = new FunctionTree();
            //if (Line[5].ElementReference != null)
            if (Line[4].ElementReference != null)
            {
                //ISyntaxTree function = Line[5].ElementReference.GetParser().GetSyntaxTree();
                ISyntaxTree function = Line[4].ElementReference.GetParser().GetSyntaxTree();
                blockFalse.Childs.Add(function);
            }
            else blockFalse = null;

            expressionParser = new ExpressionParser(Line[0]);
            expressionParser.Normalize();
            Line[0] = expressionParser.Line[0];

            ISyntaxTree leftOp = expressionParser.GetSyntaxTree();

            expressionParser = new ExpressionParser(Line[2]);
            expressionParser.Normalize();
            Line[2] = expressionParser.Line[0];

            ISyntaxTree rightOp = expressionParser.GetSyntaxTree();

            ISyntaxTree condition = GetConditionTree(Line[1].Element);
            condition.Childs.Add(leftOp);
            condition.Childs.Add(rightOp);

            ISyntaxTree tree = new IFTree(blockTrue, blockFalse);
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
                case "=":
                    return new EqualTree();
                case "!=":
                    return new NotEqualTree();
                case "<":
                    return new LessTree();
                case "<=":
                    return new LessOrEqualTree();
                case ">":
                    return new GreaterTree();
                case ">=":
                    return new GreaterOrEqual();
                default:
                    break;
            }
            return null;
        }
    }
}