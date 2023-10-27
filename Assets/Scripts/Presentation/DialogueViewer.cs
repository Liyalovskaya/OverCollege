using System;
using OC.Core;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace OC.Presentation
{
    public class DialogueViewer : DialogueViewBase
    {
        private Action _advanceHandler = null;

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            Debug.Log($"Running line {dialogueLine.TextID}");
            GameMaster.Instance.WriteLine(dialogueLine.RawText);
            _advanceHandler = requestInterrupt;
        }

        public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            _advanceHandler = null;
            Debug.Log($"Interrupted while presenting {dialogueLine.TextID}");
            onDialogueLineFinished();
        }

        public override void UserRequestedViewAdvancement()
        {
            _advanceHandler?.Invoke();
        }

        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {
            GameMaster.Instance.OnDialogueOptionSelected = onOptionSelected;
            GameMaster.Instance.DialogueOptions = dialogueOptions;
        }

        public override void DialogueStarted()
        {
            GameMaster.Instance.ClearText();
            GameMaster.Instance.ReleaseDialogue();
        }

        public override void DialogueComplete()
        {
            _advanceHandler = null;
            GameMaster.Instance.ReleaseDialogue();
        }
    }
}