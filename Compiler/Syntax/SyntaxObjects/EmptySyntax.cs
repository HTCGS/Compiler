using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class EmptySyntax : ISyntaxObject
    {
        public List<ISyntaxElement> Syntax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Line { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[] Elements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SyntaxError Check()
        {
            return SyntaxError.NoError;
        }
    }
}
