using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Compiler
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

        public Compiler(ILexer lexer, IParser parser):this()
        {
            this.Lexer = lexer;
            this.Parser = parser;
        }

        public Compiler(ILexer lexer, IParser parser, string filePath) : this(filePath)
        {
            this.Lexer = lexer;
            this.Parser = parser;
        }

        public void CodeAnalysis(string filePath = null)
        {
            Lexer lexer;
            if (filePath != null) lexer = new Lexer(filePath);
            else if (this.FilePath != null) lexer = new Lexer(this.FilePath);
            else
            {
                Console.WriteLine("Code file is undefined!");
                return;
            }
            lexer.ReadFile(lexer.FilePath);
            foreach (var item in lexer.Code)
            {
                ISyntaxObject syntaxObject = lexer.ScanFile();
                if(syntaxObject != null)
                {
                    Syntax.Add(syntaxObject);
                }
                else
                {
                    Console.WriteLine("Unnown!");
                    return;
                }
            }

        }

        public void MakeSyntaxTree()
        {
            ISyntaxTree operation = null;





        }


        public void Run()
        {


        }




    }
}
