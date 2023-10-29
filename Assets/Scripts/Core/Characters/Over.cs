using OC.Base;

namespace OC.Core.Characters
{
    public sealed class Over : Character
    {
        public override void Initialize()
        {
            base.Initialize();
            Schedule.Add(TimePeriod.EarlyMorning, "Home_OverBedroom");
            Schedule.Add(TimePeriod.Morning, "School_C1");
            Schedule.Add(TimePeriod.Noon, "School_Hall");
            Schedule.Add(TimePeriod.Afternoon, "School_C1");
            Schedule.Add(TimePeriod.Evening, "School_Gate");
            Schedule.Add(TimePeriod.Night, "Home_OverBedroom");
        }
    }
}
