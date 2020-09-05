using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    static public class Variables
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

        public static dynamic GetVariable(string name)
        {
            if (Variables.Byte.ContainsKey(name)) return Byte[name];
            if (Variables.Shortint.ContainsKey(name)) return Shortint[name];
            if (Variables.Smallint.ContainsKey(name)) return Smallint[name];
            if (Variables.Longint.ContainsKey(name)) return Longint[name];
            if (Variables.Longword.ContainsKey(name)) return Longword[name];
            if (Variables.Integer.ContainsKey(name)) return Integer[name];
            if (Variables.Real.ContainsKey(name)) return Real[name];
            if (Variables.Double.ContainsKey(name)) return Double[name];
            if (Variables.Char.ContainsKey(name)) return Char[name];
            if (Variables.String.ContainsKey(name)) return String[name];
            if (Variables.Boolean.ContainsKey(name)) return Boolean[name];
            return 0;
        }

        public static void SetVariable(string name, dynamic value)
        {
            if (Variables.Byte.ContainsKey(name)) Byte[name] = (byte) value;
            if (Variables.Shortint.ContainsKey(name)) Shortint[name] = value;
            if (Variables.Smallint.ContainsKey(name)) Smallint[name] = value;
            if (Variables.Longint.ContainsKey(name)) Longint[name] = value;
            if (Variables.Longword.ContainsKey(name)) Longword[name] = value;
            if (Variables.Integer.ContainsKey(name)) Integer[name] = value;
            if (Variables.Real.ContainsKey(name)) Real[name] = value;
            if (Variables.Double.ContainsKey(name)) Double[name] = value;
            if (Variables.Char.ContainsKey(name)) Char[name] = value;
            if (Variables.String.ContainsKey(name)) String[name] = value;
            if (Variables.Boolean.ContainsKey(name)) Boolean[name] = value;
        }
    }
}