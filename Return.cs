using Pocole.Util;

namespace Pocole
{
    [System.Serializable]
    public class Return : Runnable
    {
        public string Formula { get; private set; }
        public Return(Runnable parent, string source) : base(parent, source)
        {
            var split = source.PoSplitOnce(' ');
            if (split.Length < 2)
            {
                // NOTE: "return;" としか書かれていない場合は nullを返すことにしておく
                Formula = "null";
                return;
            }
            Formula = split[1];
        }

        protected override void Run()
        {
            var res = Util.Calc.Execute(GetParentBlock(), Formula, GetParentMethod().ReturnType);
            if (res != null)
            {
                GetParentMethod().ReturnedValue = res.Object;
            }
            GetParentMethod().SkipExecute();
        }
    }
}