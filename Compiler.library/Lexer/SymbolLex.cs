﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SymbolLex : AbstractLexerElement
    {
        private char[] Symbols = new char[]
        {
            'q',
            'w',
            'e',
            'r',
            't',
            'y',
            'u',
            'i',
            'o',
            'p',
            'a',
            's',
            'd',
            'f',
            'g',
            'h',
            'j',
            'k',
            'l',
            'z',
            'x',
            'c',
            'v',
            'b',
            'n',
            'm',
            'Q',
            'W',
            'E',
            'R',
            'T',
            'Y',
            'U',
            'I',
            'O',
            'P',
            'A',
            'S',
            'D',
            'F',
            'G',
            'H',
            'J',
            'K',
            'L',
            'Z',
            'X',
            'C',
            'V',
            'B',
            'N',
            'M',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '0',
            '=',
            '/',
            '*',
            '-',
            '+',
            '[',
            ']',
            '(',
            ')',
            ';',
            ':',
            '\'',
            '\"',
            ',',
            '.',
            '<',
            '>',
            '?',
            ' ',
            '\\'
        };

        public SymbolLex() { }

        public SymbolLex(string key) : base(key) { }

        public override Keyword GetKeyword(string input)
        {
            foreach (char item in input)
            {
                if (!Array.Exists(Symbols, ch => ch == item)) return Keyword.Unknown;
            }
            return Keyword.LanguageSymbols;
        }

        public override ISyntaxObject GetSyntaxScaner()
        {
            return null;
        }

        public SymbolType GetSymbolType(char symbol)
        {
            if ((symbol >= 65 && symbol <= 90) || (symbol >= 97 && symbol <= 122)) return SymbolType.Letter;
            if (symbol >= 48 && symbol <= 57) return SymbolType.Digit;
            if (symbol == '/' || symbol == '*' || symbol == '-' || symbol == '+') return SymbolType.Arifmetic;
            if (symbol == '(' || symbol == ')') return SymbolType.Bracket;
            if (symbol == '[' || symbol == ']') return SymbolType.SquareBracket;
            return SymbolType.Punctuation;
        }
    }

    public enum SymbolType
    {
        Letter,
        Digit,
        Arifmetic,
        Bracket,
        SquareBracket,
        Punctuation
    }
}