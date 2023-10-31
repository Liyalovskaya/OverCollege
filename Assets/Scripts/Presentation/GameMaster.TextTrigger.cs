
using System.Linq;
using OC.Core;
using OC.Core.Operations;
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

            if (WaitingForOperation)
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
            DisplayLocationMain();
        }

        public void OnLocationMove()
        {
            LeftPanel.Instance.InfoRefresh();
            DisplayLocationMain();
        }

        public void OnMoneyChanged()
        {
            LeftPanel.Instance.InfoRefresh();
        }
        
        
    }
}