namespace OC.Core.Operations
{
    public class Move : Operation
    {
        public new readonly Location Target;

        public Move(Location target, bool newline = false) : base(target)
        {
            Id = $"goTo_{target.Id}";
            Target = target;
            NewLine = newline;
        }

        public override string Content()
        {
            return $"去{Target.FullName}";
        }

        public override void Execute(GameRun gameRun)
        {
            gameRun.MoveTo(Target);
        }
    }
}