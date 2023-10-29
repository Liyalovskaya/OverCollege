using OC.Base;

namespace OC.Core.Characters
{
    public sealed class Tt : Character
    {
        public override void Initialize()
        {
            base.Initialize();
            Schedule.Add(TimePeriod.EarlyMorning, "");
            Schedule.Add(TimePeriod.Morning, "School_C2");
            Schedule.Add(TimePeriod.Noon, "School_Playground");
            Schedule.Add(TimePeriod.Afternoon, "School_C2");
            Schedule.Add(TimePeriod.Evening, "");
            Schedule.Add(TimePeriod.Night, "");
        }
    }
}
