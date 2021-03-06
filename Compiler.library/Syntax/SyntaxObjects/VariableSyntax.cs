﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class VariableSyntax : SyntaxObject
    {
        public VariableSyntax()
        {
            Syntax = new List<ISyntaxObject>
            {
                new IdSyntax(),
                new AssignSyntax("Assign", ":="),
                new ExpressionSyntax("Expression")
            };
        }

        public VariableSyntax(string context) : base(context) { }

        public VariableSyntax(bool isNullable) : base(isNullable) { }

        public override IParserElement GetParser()
        {
            VariableParser parser = new VariableParser();
            parser.Line = Elements.GetParserElements(0, 2);
            return parser;
        }
    }
}