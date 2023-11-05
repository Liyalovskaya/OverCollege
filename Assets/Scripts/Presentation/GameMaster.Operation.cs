using System.Collections.Generic;
using System.Linq;
using OC.Core;
using OC.Core.Operations;
using OC.Core.Scenes;
using UnityEngine;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        private bool _waitForOperation;

        public bool WaitForOperation
        {
            get => _waitForOperation;
            set
            {
                if (_waitForOperation != value)
                {
                    _waitForOperation = value;
                    OnTextChanged();
                }
            }
        }

        public void EnterScene(OcScene scene)
        {
            ClearText();
            Scene = scene;
            MainText = scene.MainText;
            Operations = scene.Operations;

            WaitForOperation = true;
        }

        public void SelectOperation(int idx)
        {
            WaitForOperation = false;
            _lastAction = (string)Operations[idx].Content().Clone();
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
                var actualIdx = 0;
                str += "\n\n=====[互动]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if(operations[i] is not Interact) continue;
                    if (i == HighlightOptionId)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link></color>";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link>";
                    }

                    if (actualIdx % 4 == 4 - 1)
                    {
                        str += "\n";
                    }
                    else
                    {
                        str += "\t\t";
                    }
                    actualIdx++;
                }
            }

            if (operations.Any(x => x is Move))
            {
                var actualIdx = 0;
                str += "\n\n=====[移动]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if(operations[i] is not Move) continue;
                    if (i == HighlightOptionId)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link></color>";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link>";
                    }

                    if (actualIdx % 4 == 4 - 1)
                    {
                        str += "\n";
                    }
                    else
                    {
                        str += "\t\t";
                    }

                    actualIdx++;
                }
            }
            

            if (operations.Any(x => x is Activity) )
            {
                var actualIdx = 0;
                str += "\n\n=====[行动]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if(operations[i] is not Activity) continue;
                    if (i == HighlightOptionId)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link></color>";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link>";
                    }

                    if (actualIdx % 4 == 4 - 1)
                    {
                        str += "\n";
                    }
                    else
                    {
                        str += "\t\t";
                    }

                    actualIdx++;
                }
            }
            
            if (operations.Any(x => x is DialogueOption) )
            {
                var actualIdx = 0;
                str += "\n\n=====[对话]===========================\n";
                for (int i = 0; i < operations.Count; i++)
                {
                    if(operations[i] is not DialogueOption) continue;
                    if (i == HighlightOptionId)
                    {
                        str +=
                            $"<color=#00FF00><link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link></color>";
                    }
                    else
                    {
                        str += $"<link=\"{i}\">{actualIdx + 1}. {operations[i].Content()}</link>";
                    }

                    if (actualIdx % 4 == 4 - 1)
                    {
                        str += "\n";
                    }
                    else
                    {
                        str += "\t\t";
                    }

                    actualIdx++;
                }
            }


            return str;
        }
    }
}