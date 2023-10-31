using System.Collections.Generic;
using OC.ConfigData;
using OC.Core.Operations;
using UnityEngine;

namespace OC.Core
{
    public class Location : GameEntity
    {

        public GameRun GameRun;
        
        public string FullName;

        public LocationConfig Config;
        
        public readonly List<Character> Characters = new();
        
        public readonly List<Location> Connections = new();

        public List<Activity> Activities = new();


        public override void Initialize()
        {
            base.Initialize();
            Config = LocationConfig.FromId(Id);
            FullName = Config.FullName;
        }
        

    }
}
