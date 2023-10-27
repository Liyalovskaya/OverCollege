
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
        }

        public void OnLocationMove()
        {
            DisplayLocationMain();
        }

        public void OnMoneyChanged()
        {
            LeftPanel.Instance.InfoRefresh();
        }
        
        
    }
}