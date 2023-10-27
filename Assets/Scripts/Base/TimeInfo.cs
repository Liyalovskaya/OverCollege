namespace OC.Base
{
    public class TimeInfo
    {
        public int Year = 1;
        public int Month = 8;
        public int Day = 30;
        public int TotalDay = 1;
        
        public TimePeriod TimePeriod = TimePeriod.Morning;

        public void NextDay()
        {
            TotalDay++;
            if (++Day > 30)
            {
                Day = 1;
                TimePeriod = TimePeriod.Morning;
                if (++Month > 12)
                {
                    Year++;
                    Month = 1;
                }
            }
        }

        public TimeInfo(int year, int month, int day, TimePeriod period)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            TimePeriod = period;
        }

        public string GetPeriodString()
        {
            return TimePeriod switch
            {
                TimePeriod.EarlyMorning => "清晨",
                TimePeriod.Morning => "上午",
                TimePeriod.Noon => "中午",
                TimePeriod.Afternoon => "下午",
                TimePeriod.Evening => "夜晚",
                TimePeriod.Night => "深夜",
                _ => "ERROR"
            };
        }

        public string DayInWeek()
        {
            return (TotalDay % 7) switch
            {
                1 => "星期一",
                2 => "星期二",
                3 => "星期三",
                4 => "星期四",
                5 => "星期五",
                6 => "星期六",
                0 => "星期日",
                _ => "ERROR"
            };
        }


    }
    
    public enum TimePeriod
    {
        EarlyMorning,
        Morning,
        Noon,
        Afternoon,
        Evening,
        Night,
    }
}