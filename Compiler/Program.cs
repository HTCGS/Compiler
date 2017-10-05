using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            VariableLex variableLex = new VariableLex();
            Variables.Boolean.Add("a", true);
            Console.WriteLine(variableLex.GetKeyword("a"));



            Console.ReadKey();
        }
    }
}
