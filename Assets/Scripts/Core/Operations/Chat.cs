namespace OC.Core.Operations
{
    public class Chat : Interact
    {
        public new readonly Character Target;
        public Chat(Character target) : base(target)
        {
            Id = $"chatTo_{target.Id}";
            Target = target;
        }
        
        public override string Content()
        {
            return $"和{Target.FullName()}聊天";
        }

        public override void Execute(GameRun gameRun)
        {
            gameRun.RunDialogue($"{Target.Id}_Chat_{gameRun.CurrentLocation.Id}_{gameRun.TimeInfo.TimePeriod.ToString()}");
        }
    }
}
