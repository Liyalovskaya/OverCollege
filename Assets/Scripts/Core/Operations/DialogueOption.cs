using Yarn.Unity;

namespace OC.Core.Operations
{
    public class DialogueOption : Operation
    {
        public readonly Yarn.Unity.DialogueOption Option;
        public int Index { get; set; }

        public DialogueOption(Yarn.Unity.DialogueOption option, int idx) : base()
        {
            Option = option;
            Index = idx;
        }
        public override string Content()
        {
            return $"{Option.Line.RawText}";
        }
        public override void Execute(GameRun gameRun)
        {
            
        }
        
    }
}