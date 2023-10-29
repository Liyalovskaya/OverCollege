using OC.Base;

namespace OC.Core.Characters
{
    public sealed class My : Character
    {
        public override void Initialize()
        {
            base.Initialize();
            Schedule.Add(TimePeriod.EarlyMorning, "");
            Schedule.Add(TimePeriod.Morning, "School_C1");
            Schedule.Add(TimePeriod.Noon, "School_Hall");
            Schedule.Add(TimePeriod.Afternoon, "School_C1");
            Schedule.Add(TimePeriod.Evening, "");
            Schedule.Add(TimePeriod.Night, "");
        }
    }
}
