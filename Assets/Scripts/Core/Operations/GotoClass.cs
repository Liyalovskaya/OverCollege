namespace OC.Core.Operations
{
    public class GotoClass : Activity
    {
        public override string Content()
        {
            return "上课";
        }

        public override void Execute(GameRun gameRun)
        {
            gameRun.NextTimePeriod();
        }

    }
}