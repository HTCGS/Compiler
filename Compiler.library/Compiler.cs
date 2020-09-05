using System;
using System.Collections.Generic;

namespace Compiler
{
    public class Compiler
    {
        public ILexer Lexer { get; set; }
        public IParser Parser { get; set; }

        public List<ISyntaxObject> Syntax { get; set; }
        public List<ISyntaxTree> Programs { get; set; }

        public string FilePath { get; set; }

        public Compiler()
        {
            this.Syntax = new List<ISyntaxObject>();
            this.Programs = new List<ISyntaxTree>();
        }

        public Compiler(string filePath) : this()
        {
            this.FilePath = filePath;
        }

        public Compiler(ILexer lexer, IParser parser) : this()
        {
            this.Lexer = lexer;
            this.Parser = parser;
        }

        public Compiler(ILexer lexer, IParser parser, string filePath) : this(filePath)
        {
            this.Lexer = lexer;
            this.Parser = parser;
        }

        public bool CodeAnalysis(string filePath = null)
        {
            int line = 0;
            Lexer lexer;
            if (filePath != null) lexer = new Lexer(filePath);
            else if (this.FilePath != null) lexer = new Lexer(this.FilePath);
            else
            {
                Console.WriteLine("Code file is undefined!");
                return false;
            }
            lexer.ReadFile(lexer.FilePath);
            foreach (var item in lexer.Code)
            {
                line++;
                ISyntaxObject syntaxObject = lexer.ScanFile();
                if (syntaxObject != null)
                {
                    Syntax.Add(syntaxObject);
                }
                else
                {
                    Console.WriteLine("Unnown lex at line {0}!", line);
                    return false;
                }
            }
            return true;
        }

        public SyntaxError CheckSyntax()
        {
            int line = 0;
            foreach (var item in Syntax)
            {
                line++;
                SyntaxError syntaxError = item.Check();
                if (syntaxError != SyntaxError.NoError)
                {
                    Console.WriteLine(syntaxError + " at line " + line);
                    return syntaxError;
                }
            }
            return SyntaxError.NoError;
        }

        public void MakeSyntaxTree()
        {
            Parser.Syntax = this.Syntax;
            Parser.Check();
            foreach (var item in Parser.ParserElements)
            {
                SyntaxError syntaxError = item.Check();
                if (syntaxError == SyntaxError.NoError)
                {
                    item.Normalize();
                    Programs.Add(item.GetSyntaxTree());
                }
                else
                {
                    Console.WriteLine(syntaxError);
                    return;
                }
            }
        }

        public void Run()
        {
            foreach (var item in Programs)
            {
                if (item != null) item.Execute();
            }
        }
    }
}