using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    static class Variables
    {
        public static Dictionary<string, byte> Byte { get; set; }
        public static Dictionary<string, sbyte> Shortint { get; set; }
        public static Dictionary<string, short> Smallint { get; set; }
        public static Dictionary<string, int> Longint { get; set; }
        public static Dictionary<string, uint> Longword { get; set; }
        public static Dictionary<string, int> Integer { get; set; }

        public static Dictionary<string, float> Real { get; set; }
        public static Dictionary<string, double> Double { get; set; }

        public static Dictionary<string, char> Char { get; set; }
        public static Dictionary<string, string> String { get; set; }

        public static Dictionary<string, bool> Boolean { get; set; }

        static Variables()
        {
            Byte = new Dictionary<string, byte>();
            Shortint = new Dictionary<string, sbyte>();
            Smallint = new Dictionary<string, short>();
            Longint = new Dictionary<string, int>();
            Longword = new Dictionary<string, uint>();
            Integer = new Dictionary<string, int>();
            Real = new Dictionary<string, float>();
            Double = new Dictionary<string, double>();
            Char = new Dictionary<string, char>();
            String = new Dictionary<string, string>();
            Boolean = new Dictionary<string, bool>();
        }
    }
}
