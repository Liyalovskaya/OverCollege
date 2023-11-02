using System.Collections.Generic;
using OC.ConfigData;
using OC.Core.Operations;
using UnityEngine;

namespace OC.Core
{
    public class Scene : GameEntity
    {
        public GameRun GameRun;


        public List<Operation> Operations;
        public override void Initialize()
        {
            base.Initialize();
            Config = LocationConfig.FromId(Id);
            FullName = Config.FullName;
        }
        

    }
}
