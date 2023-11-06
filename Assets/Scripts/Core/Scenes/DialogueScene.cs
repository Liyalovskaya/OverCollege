using System;
using System.Collections.Generic;
using System.Linq;
using OC.Core.Operations;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using DialogueOption = OC.Core.Operations.DialogueOption;

namespace OC.Core.Scenes
{
    public class DialogueScene : OcScene
    {
        public Action<int> OnDialogueOptionSelected = null;
        public Yarn.Unity.DialogueOption[] DialogueOptions = null;
        public readonly List<string> DialogueLines = new();

        public override void Update()
        {
            ReleaseText();
            ShowLastOperation();
            foreach (var str in DialogueLines)
            {
                WriteLine(str);
            }
            
            Operations.Clear();
            if (DialogueOptions != null)
            {
                for (var i = 0; i < DialogueOptions.Length; i++)
                {
                    Operations.Add(new DialogueOption(DialogueOptions[i], i));
                }
            }
            
            GameRun.TextTrigger?.UpdateViewer();
        }

        public void RunLine(LocalizedLine line)
        {
            RunLine(line.RawText);
        }

        public void RunLine(string line)
        {
            DialogueLines.Add(line);
            Update();
        }
        

        public void RunOption(Yarn.Unity.DialogueOption[] dialogueOptions, Action<int> onDialogueOptionSelected)
        {
            DialogueOptions = dialogueOptions;
            OnDialogueOptionSelected = onDialogueOptionSelected;
            Update();
        }

        public void ReleaseDialogue()
        {
            OnDialogueOptionSelected = null;
            DialogueOptions = null;
            ReleaseText();
            DialogueLines.Clear();
        }


        public override void SelectOption(int idx)
        {
            base.SelectOption(idx);
            DialogueLines.Add($"(你选择了[{GameRun.LastOperationContent}])");
            OnDialogueOptionSelected?.Invoke(idx);
            OnDialogueOptionSelected = null;
            Operations.Clear();
        }

        public override string OperationText(int highlightIdx = -1)
        {
            if (Operations.Count == 0)
            {
                return "";
            }
            var str = "";
            str += StringHelper.DisplayOption<DialogueOption>(Operations, "对话", highlightIdx);
            return str;
        }


        public DialogueScene(GameRun gameRun) : base(gameRun)
        {
        }
    }
}
