namespace OC.Core.Operations
{
    public class Chat : Operation
    {
        public Character Target;
        public Chat(Character target)
        {
            Id = $"chatTo_{target.Id}";
            Target = target;
        }

        public override void Execute(GameRun gameRun)
        {

        }
    }
}
