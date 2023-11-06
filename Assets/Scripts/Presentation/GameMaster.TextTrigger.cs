using System;
using System.Linq;
using OC.Core;
using OC.Core.Operations;
using OC.Core.Scenes;
using UnityEngine;

namespace OC.Presentation
{
    public partial class GameMaster : IGameRunTextTrigger
    {
        public void UpdateViewer()
        {
            if (CurrentScene == null)
            {
                throw new ArgumentException("Null CurrentScene");
            }

            mainTextUGUI.text = CurrentScene.MainText + CurrentScene.OperationText(HighlightOptionId);    
        }

        public void OnTimeChanged()
        {
            LeftPanel.Instance.InfoRefresh();
            foreach (var character in GameRun.Characters)
            {
                var location = GameRun.GetLocation(character.Schedule[GameRun.TimeInfo.TimePeriod]);
                character.MoveTo(location);
            }

            LocationScene.Update();
            UpdateViewer();
        }

        public void OnLocationMove()
        {
            LeftPanel.Instance.InfoRefresh();
            LocationScene.Update();
            UpdateViewer();
        }

        public void OnMoneyChanged()
        {
            LeftPanel.Instance.InfoRefresh();
        }

        public void RunDialogue(string id)
        {
            DialogueScene.ReleaseDialogue();
            CurrentScene = DialogueScene;
            if (DialogueRunner.yarnProject.NodeNames.Contains(id))
            {
                DialogueRunner.StartDialogue(id);
            }
            else
            {
                DialogueRunner.StartDialogue("NodeNotFound");
                DialogueScene.RunLine($"对话Id[{id}]不存在");
            }



        }
        
        public void OnCharacterMove()
        {
            LocationScene.Update();
            UpdateViewer();
        }
        
        
    }
}