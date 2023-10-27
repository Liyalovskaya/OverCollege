using System.Collections.Generic;
using OC.ConfigData;
using UnityEngine;

namespace OC.Core
{
    public class Location : GameEntity
    {
        public string FullName;

        public LocationConfig Config;
        
        public readonly List<Character> Characters = new();
        
        public readonly List<Location> Connections = new();
        
        
        public override void Initialize()
        {
            base.Initialize();
            Config = LocationConfig.FromId(Id);
            FullName = Config.FullName;
        }
        

    }
}
