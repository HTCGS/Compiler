using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    // public interface ILexer
    // {
    //     List<ILexerElement> LexElements { get; set; }
    //     string[] ReadFile(string path);
    //     ISyntaxObject ScanFile();
    // }

    public interface ILexer
    {
        List<string> Keywods {get; set;}
        List<List<Lexeme>> LexTable {get; set;}
        List<string> ProgramText {get; set;}
        List<List<Lexeme>> CreateLexTable(List<string> code);
    }

    public class Lexeme
    {
        string Text;
        LexemeType LexemeType; 
    }

    public enum LexemeType
    {
        Unknown,
        Keyword,
        Letter,
        Number
    }
}