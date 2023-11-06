using System;
using OC.Core;
using OC.Core.Scenes;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace OC.Presentation
{
    public class DialogueViewer : DialogueViewBase
    {
        // private Action _advanceHandler = null;

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            GameMaster.Instance.DialogueScene.RunLine(dialogueLine);
            onDialogueLineFinished();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            GameMaster.Instance.DialogueScene.RunOption(dialogueOptions, onOptionSelected);
        }

        public override void DialogueStarted()
        {
            
        }

        public override void DialogueComplete()
        {
            // _advanceHandler = null;
            GameMaster.Instance.DialogueScene.ReleaseDialogue();
            GameMaster.Instance.CurrentScene = GameMaster.Instance.LocationScene;
        }
    }
}