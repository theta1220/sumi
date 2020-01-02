using System;
using System.Text.RegularExpressions;

namespace Pocole
{
    public class Value
    {
        public Type ValueType { get { return Object.GetType(); } }
        public string Name { get; private set; }
        public object Object { get; private set; }

        public bool Initialize()
        {
            return true;
        }

        public bool Initialize(string name)
        {
            Name = name;
            return true;
        }

        public bool Initialize(string name, string value)
        {
            if (!Initialize(name)) { Log.InitError(); return false; }
            Object = value;

            if (ValueType == typeof(int))
            {
                Object = int.Parse(value);
            }
            else
            {
                Object = value;
            }
            return true;
        }

        public void SetValue(object Value, Type type)
        {
            SetValue(Value);
        }

        public void SetValue(object value)
        {
            Object = value;
        }

        public static Type GetValueType(string source, Block parentBlock = null)
        {
            source = source.Replace(" ", "");
            var value = Util.String.SplitAny(source, "+-*/");

            var resInt = 0;
            if (int.TryParse(value[0], out resInt))
            {
                return typeof(int);
            }
            else if (parentBlock != null && parentBlock.FindValue(value[0]) != null)
            {
                return parentBlock.FindValue(value[0]).ValueType;
            }
            else
            {
                return typeof(string);
            }
        }
    }
}