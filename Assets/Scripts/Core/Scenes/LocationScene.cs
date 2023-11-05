using System.Collections.Generic;
using System.Linq;
using OC.Core.Operations;

namespace OC.Core.Scenes
{
    public class LocationScene : OcScene
    {
        public Location Location { get; set; }

        public LocationScene(Location location)
        {
            Location = location;
        }

        public override void Enter()
        {
            MainText = $"你当前在{Location.FullName}。\n";
            if (Location.Characters.Count != 0)
            {
                MainText += "你看到";
                for (int i = 0; i < Location.Characters.Count; i++)
                {
                    Operations.Add(new Chat(Location.Characters[i]));
                    MainText += $"{Location.Characters[i].FullName()}";
                    if (i != Location.Characters.Count - 1)
                    {
                        MainText += "、";
                    }
                }

                MainText += "也在这里。\n";
            }
            else
            {
                MainText += "这里没有别人。\n";
            }

            RefreshOperations();
        }


        public override void RefreshOperations()
        {
            base.RefreshOperations();
            var connections = Location.Connections;
            foreach (var connection in connections)
            {
                var option = new Move(connection);
                Operations.Add(option);
            }

            foreach (var activity in Location.Activities)
            {
                Operations.Add(activity);
            }
        }
    }
}