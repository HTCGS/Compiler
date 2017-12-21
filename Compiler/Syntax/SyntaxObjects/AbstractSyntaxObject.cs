using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    abstract class AbstractSyntaxObject : ISyntaxObject
    {
        public List<ISyntaxObject> Syntax { get; set; }
        public string Context { get; set; }
        public List<string> Elements { get; set; }
        public bool IsNullable { get; set; }
        public SyntaxType Type { get; set; }

        public AbstractSyntaxObject()
        {
            this.Context = string.Empty;
            this.Elements = new List<string>();
            this.IsNullable = false;
            this.Type = SyntaxType.SyntaxElement;
        }

        public AbstractSyntaxObject(bool isNullable) : this()
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(string context) : this()
        {
            this.Context = context;
        }

        public AbstractSyntaxObject(string context, bool isNullable) : this(context)
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(SyntaxType type) :this()
        {
            this.Type = type;
        }

        public AbstractSyntaxObject(SyntaxType type, bool isNullable) : this(type)
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(string context, SyntaxType type) : this(context)
        {
            this.Type = type;
        }

        public AbstractSyntaxObject(string context, SyntaxType type, bool isNullable) : this(context, type)
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(SyntaxType type, params string[] elements) : this(type)
        {
            foreach (string element in elements)
            {
                this.Elements.Add(element);
            }
        }

        public AbstractSyntaxObject(SyntaxType type, bool isNullable, params string[] elements) : this(type, elements)
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(string context, params string[] elements) :this(context)
        {
            foreach (string element in elements)
            {
                this.Elements.Add(element);
            }
        }

        public AbstractSyntaxObject(string context, bool isNullable, params string[] elements) : this(context, elements)
        {
            this.IsNullable = isNullable;
        }

        public AbstractSyntaxObject(string context, SyntaxType type, params string[] elements) : this(context, elements)
        {
            this.Type = type;
        }

        public AbstractSyntaxObject(string context, SyntaxType type, bool isNullable, params string[] elements) : this(context, type, elements)
        {
            this.IsNullable = isNullable;
        }

        public abstract SyntaxError Check();
        public abstract SyntaxError Check(string context);
        public abstract IParserElement GetParser();

        public virtual SyntaxType GetSyntaxType()
        {
            return this.Type;
        }
    }
}
