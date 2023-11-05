
using System.Linq;
using OC.Core;
using OC.Core.Operations;
using OC.Core.Scenes;
using UnityEngine;

namespace OC.Presentation
{
    public partial class GameMaster : IGameRunTextTrigger
    {
        public void OnTextChanged()
        {
            var str = MainText;
            if (WaitForDialogueOption)
            {
                str += DialogueOptionText(DialogueOptions);
            }

            if (WaitForOperation)
            {
                str += OperationText(Operations);
            }
            
            mainTextUGUI.text = str;
        }

        public void OnTimeChanged()
        {
            LeftPanel.Instance.InfoRefresh();
            foreach (var character in GameRun.Characters)
            {
                var location = GameRun.GetLocation(character.Schedule[GameRun.TimeInfo.TimePeriod]);
                character.MoveTo(location); 
            }
            MainText = Scene.MainText;
        }

        public void OnLocationMove()
        {
            LeftPanel.Instance.InfoRefresh();
            Scene = new LocationScene(GameRun.CurrentLocation);
            MainText = Scene.MainText;
        }

        public void OnMoneyChanged()
        {
            LeftPanel.Instance.InfoRefresh();
        }

        public void RunDialogue(string id)
        {
            DialogueRunner.StartDialogue(id);
            Scene = new DialogueScene(DialogueRunner.Dialogue);
            inDialogue = true;
        }

        public void SelectDialogueOption(int idx)
        {
            
            WriteLine($"(你选择了[{DialogueOptions[idx].Line.RawText}])");
            _lastAction = (string)DialogueOptions[idx].Line.RawText.Clone();
            
            OnDialogueOptionSelected?.Invoke(idx);
            OnDialogueOptionSelected = null;
            WaitForDialogueOption = false;
            DialogueOptions = null;
        }
        
        
    }
}