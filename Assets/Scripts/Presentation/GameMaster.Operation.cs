using System.Collections.Generic;
using System.Linq;
using OC.Core;
using OC.Core.Operations;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        private bool _waitingForOperation;

        public bool WaitingForOperation
        {
            get => _waitingForOperation;
            set
            {
                if (_waitingForOperation != value)
                {
                    _waitingForOperation = value;
                    OnTextChanged();
                }
            }
        }

        public void DisplayLocationMain()
        {
            ClearText(false);
            WriteLine($"你当前在{GameRun.CurrentLocation.FullName}。");
            Operations.Clear();

            var characters = GameRun.Characters.Where(x => x.Location == GameRun.CurrentLocation).ToList();

            if (characters.Count != 0)
            {
                var nameStr = "你看到";
                for (int i = 0; i < characters.Count; i++)
                {
                    Operations.Add(new Chat(characters[i]));
                    nameStr += $"{characters[i].FullName()}";
                    if (i != characters.Count - 1)
                    {
                        nameStr += "、";
                    }
                }

                nameStr += "也在这里。";
                WriteLine(nameStr);
            }
            else
            {
                WriteLine("这里没有别人。");
            }


            var connections = GameRun.CurrentLocation.Connections;
            for (int i = 0; i < connections.Count; i++)
            {
                var option = new Move(connections[i]);
                Operations.Add(option);
            }

            WaitingForOperation = true;
        }

        public void SelectOperation(int idx)
        {
            WaitingForOperation = false;
            _lastAction = (string) Operations[idx].Content().Clone();
            Operations[idx].Execute(GameRun);
        }

        public string OperationText(List<Operation> operations)
        {
            if (operations.Count == 0)
            {
                return "NULL OPERATIONS";
            }

            var str = "";

            if (operations.Any(x => x is Interact))
            {
                str += "\n\n=====[互动]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if (operations[i] is not Interact) continue;
                    if (i == HighlightOptionIdx)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{i + 1}. {operations[i].Content()}</link></color>\t\t";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{i + 1}. {operations[i].Content()}</link>\t\t";
                    }

                    if (i % 4 == 4 - 1) str += "\n";
                }
            }

            if (operations.Any(x => x is Move))
            {
                str += "\n\n=====[移动]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if (operations[i] is not Move) continue;
                    if (i == HighlightOptionIdx)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{i + 1}. {operations[i].Content()}</link></color>\t\t";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{i + 1}. {operations[i].Content()}</link>\t\t";
                    }

                    if (i % 4 == 4 - 1) str += "\n";
                }
            }


            return str;
        }
    }
}