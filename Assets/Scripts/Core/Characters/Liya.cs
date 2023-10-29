using OC.Base;

namespace OC.Core.Characters
{
    public sealed class Liya : Character
    {
        public override void Initialize()
        {
            base.Initialize();
            Schedule.Add(TimePeriod.EarlyMorning, "");
            Schedule.Add(TimePeriod.Morning, "School_C2");
            Schedule.Add(TimePeriod.Noon, "School_Roof");
            Schedule.Add(TimePeriod.Afternoon, "School_C2");
            Schedule.Add(TimePeriod.Evening, "School_Gate");
            Schedule.Add(TimePeriod.Night, "");
        }
    }
}
