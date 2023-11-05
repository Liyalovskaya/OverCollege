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
            Debug.Log($"Running line {dialogueLine.TextID}");
            GameMaster.Instance.Scene.WriteLine(dialogueLine.RawText);
            onDialogueLineFinished();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            GameMaster.Instance.OnDialogueOptionSelected = onOptionSelected;
            var dialogueScene = (DialogueScene)GameMaster.Instance.Scene;
            dialogueScene.RunOption(dialogueOptions);
        }

        public override void DialogueStarted()
        {
            GameMaster.Instance.ClearText();
        }

        public override void DialogueComplete()
        {
            // _advanceHandler = null;
            GameMaster.Instance.OnDialogueOptionSelected = null;
        }
    }
}