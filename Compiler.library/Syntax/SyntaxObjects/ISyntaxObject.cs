﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface ISyntaxObject
    {
        List<ISyntaxObject> Syntax { get; set; }
        string Context { get; set; }
        SyntaxElement Elements { get; set; }
        bool IsNullable { get; set; }
        SyntaxType Type { get; set; }
        SyntaxError Check();
        SyntaxError Check(string context);
        SyntaxType GetSyntaxType();
        IParserElement GetParser();
    }

    public enum SyntaxType
    {
        SyntaxElement,
        keyword,
        ID,
        Expression,
        Assign,
        Function,
        Symbol,
        Operand,
        ArithmeticSymbol,
        Bracket
    }

    public enum SyntaxError
    {
        NoError,
        SyntaxError,
        LostBracket,
        UnknownOperation,
        LostOperation,
        UndefinedVariable,
        LostOperand,
        UnknownID
    }
}