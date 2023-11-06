using System;
using System.Collections.Generic;
using OC.Base;
using OC.ConfigData;

namespace OC.Core
{
    public class Character : GameEntity
    {
        public string FirstName;
        public string LastName;
        public string CalledName;
        public string CallPlayerName;

        public float Flavor = 0;

        public Dictionary<TimePeriod, string> Schedule = new();

        public CharacterConfig Config;

        public Location Location;

        public override void Initialize()
        {
            base.Initialize();
            Config = CharacterConfig.FromId(Id);
            FirstName = Config.FirstName;
            LastName = Config.LastName;
            CallPlayerName = Config.CallPlayer;
        }

        public void EnterGameRun(GameRun gameRun)
        {
            GameRun = gameRun;
            GameRun.Characters.Add(this);
        }

        public virtual void MoveTo(Location location)
        {
            Location?.Characters.Remove(this);
            Location = location;
            Location?.Characters.Add(this);
            GameRun.OnCharacterMove();
        }

        public string FullName()
        {
            return $"{FirstName}{LastName}";
        }
        
    }
}
