using Pocole.Util;

namespace Pocole
{
    public enum ProcessType
    {
        If,
        ElseIf,
        Else,
    }

    [System.Serializable]
    public class If : Block
    {
        public ProcessType ProcessType { get; private set; }
        public string Formula { get; private set; }

        public If(Runnable parent, string source) : base(parent, source.PoExtract('{', '}'))
        {
            var name = source.PoSplitOnce('(')[0].PoRemove(' ');
            if (name == "if") ProcessType = ProcessType.If;
            else if (name == "elseif") ProcessType = ProcessType.ElseIf;
            else ProcessType = ProcessType.Else;
            Formula = source.PoExtract('(', ')').PoRemove(' ');
        }

        public override void OnEntered()
        {
            if (ProcessType == ProcessType.Else)
            {
                if (GetParentBlock().LastIfResult)
                {
                    SkipExecute();
                }
            }
            else
            {
                if (ProcessType == ProcessType.If)
                {
                    var res = (bool)Util.Calc.Execute(GetParentBlock(), Formula, typeof(bool)).Object;
                    if (!res)
                    {
                        SkipExecute();
                    }
                    GetParentBlock().LastIfResult = res;
                }
                else if (ProcessType == ProcessType.ElseIf)
                {
                    if (GetParentBlock().LastIfResult)
                    {
                        SkipExecute();
                    }
                    else
                    {
                        var res = (bool)Util.Calc.Execute(GetParentBlock(), Formula, typeof(bool)).Object;
                        if (!res)
                        {
                            SkipExecute();
                        }
                        GetParentBlock().LastIfResult = res;
                    }
                }
            }
        }
    }
}