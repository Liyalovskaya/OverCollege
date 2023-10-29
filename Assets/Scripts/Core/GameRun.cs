using System;
using System.Collections.Generic;
using System.Linq;
using OC.Base;

namespace OC.Core
{
    public class GameRun
    {
        public readonly TimeInfo TimeInfo = new TimeInfo(1, 8, 30, TimePeriod.EarlyMorning);
        public readonly List<Location> Locations = new List<Location>();
        public List<Character> Characters = new List<Character>();

        public Location CurrentLocation;

        public float Money = 1000;
        
        public IGameRunTextTrigger TextTrigger { get; set; }

        public void NextDay()
        {
            TimeInfo.NextDay();
            TextTrigger?.OnTimeChanged();
        }
        
        public void NextTimePeriod()
        {
            if (TimeInfo.TimePeriod == TimePeriod.Night)
            {
                NextDay();
            }
            TimeInfo.TimePeriod = (TimePeriod)((int)TimeInfo.TimePeriod + 1);
            TextTrigger?.OnTimeChanged();
        }
        
        
        public void GainMoney(float value)
        {
            Money += value;
            TextTrigger?.OnMoneyChanged();
        }

        public void MoveTo(Location location)
        {
            CurrentLocation = location;
            TextTrigger?.OnLocationMove();
        }

        public Location GetLocation(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return Locations.First(x => x.Id == id);

        }
        
    }
}
