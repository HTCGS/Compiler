using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class VariableLex : ILexerElement
    {
        public string Key { get; private set; }

        public VariableLex()
        {
            this.Key = "Variable";
        }

        public VariableLex(string key)
        {
            this.Key = key;
        }

        public Keyword GetKeyword(string input)
        {
            if (Variables.Byte.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Shortint.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Smallint.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Longint.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Longword.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Integer.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Real.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Double.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Char.ContainsKey(input)) return Keyword.Variable;
            if (Variables.String.ContainsKey(input)) return Keyword.Variable;
            if (Variables.Boolean.ContainsKey(input)) return Keyword.Variable;
            return Keyword.Unknown;
        }

        public ISyntaxObject GetSyntaxScaner()
        {
            ISyntaxObject syntaxObject = new VariableSyntax();
            syntaxObject.Elements[0] = this.Key;
            return syntaxObject;
        }
    }
}