namespace OC.Core.Operations
{
    public class Interact : Operation
    {
        public new readonly Character Target;

        public Interact(Character target) : base()
        {
            Id = $"interactTo_{target.Id}";
            Target = target;
        }

        public override string Content()
        {
            return $"和{Target.FullName()}互动";
        }

        public override void Execute(GameRun gameRun)
        {
        }
    }
}