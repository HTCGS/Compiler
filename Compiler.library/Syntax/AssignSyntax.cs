using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class AssignSyntax : AbstractSyntaxObject
    {
        public AssignSyntax() { }

        public AssignSyntax(string context) : base(context) { }

        public AssignSyntax(string context, params string[] elements) : base(context, elements) { }

        public override SyntaxError Check(string context)
        {
            this.Context = context;
            return Check();
        }

        public override SyntaxError Check()
        {
            if (!Elements.HasSign(Context)) return SyntaxError.LostOperation;
            return SyntaxError.NoError;
        }

        public override IParserElement GetParser()
        {
            return null;
        }

        public override SyntaxType GetSyntaxType()
        {
            return SyntaxType.Assign;
        }
    }
}