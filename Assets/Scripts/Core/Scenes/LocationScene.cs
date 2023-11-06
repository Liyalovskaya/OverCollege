using System;
using System.Collections.Generic;
using System.Linq;
using OC.Core.Operations;

namespace OC.Core.Scenes
{
    public class LocationScene : OcScene
    {
        public Location Location => GameRun.CurrentLocation;

        public override void Update()
        {
            ReleaseText();
            ShowLastOperation();
            WriteLine($"你当前在{Location.FullName}。");
            if (Location.Characters.Count != 0)
            {
                Write("你看到");
                for (int i = 0; i < Location.Characters.Count; i++)
                {
                    Operations.Add(new Chat(Location.Characters[i]));
                    Write($"{Location.Characters[i].FullName()}");
                    if (i != Location.Characters.Count - 1)
                    {
                        Write("、");
                    }
                }

                WriteLine("也在这里。");
            }
            else
            {
                WriteLine("这里没有别人。");
            }

            Operations.Clear();
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

            foreach (var character in Location.Characters)
            {
                Operations.Add(new Chat(character));
            }
        }

        public override string OperationText(int highlightIdx = -1)
        {
            if (Operations.Count == 0)
            {
                throw new ArgumentException("Null Operations");
            }

            var str = "";
            str += StringHelper.DisplayOption<Interact>(Operations, "互动", highlightIdx);
            str += StringHelper.DisplayOption<Activity>(Operations, "行为", highlightIdx);
            str += StringHelper.DisplayOption<Move>(Operations, "移动", highlightIdx);
            return str;
        }

        public LocationScene(GameRun gameRun) : base(gameRun)
        {
        }
    }
}