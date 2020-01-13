using System;
using System.Linq;

namespace Pocole
{
    [Serializable]
    public class Term : Runnable
    {
        public Term(Runnable parent, string source) : base(parent, source)
        {
        }

        public override void OnEntered()
        {
            var methodName = ExtractMethodName(Source);
            var className = ExtractClassName(Source);

            if (methodName == "SystemCall") Runnables.Add(new SystemCaller(this, Source));
            else if (IsSetter(Source)) Runnables.Add(new ValueSetter(this, Source));
            else if (IsMethod(Source)) Runnables.Add(new MethodCaller(this, Source));
            else if (GetParentBlock().FindClass(className) != null) Runnables.Add(new ClassInstantiator(this, Source));
            else throw new System.Exception(string.Format("理解できないTerm {0}", Source));
        }

        public override void OnLeaved()
        {
            Runnables.Clear();
        }

        //! ... Hoge(args)... のような文字列から メソッド名を取り出してくれる
        public static string ExtractMethodName(string source)
        {
            var buf = Util.String.PoSplit(Util.String.PoCut(source, '('), ' ');
            return buf.Last();
        }

        //! var hoge = foo; とか hoge = foo; のような文字列から 変数名を取り出してくれる
        public static string ExtractValueName(string source)
        {
            var buf = Util.String.PoSplit(Util.String.PoCut(source, '='), ' ');
            return Util.String.PoRemove(buf.Last(), ' ');
        }

        public static bool IsSetter(string source)
        {
            return Util.String.PoRemoveString(source).Contains("=");
        }

        public static bool IsMethod(string source)
        {
            return Util.String.PoRemoveString(source).Contains("(");
        }

        //! Hoge hoge; のような文字列からクラス名を取り出してくれる
        public static string ExtractClassName(string source)
        {
            return Util.String.PoRemove(Util.String.PoSplit(source, ' ').First(), ' ');
        }
    }
}