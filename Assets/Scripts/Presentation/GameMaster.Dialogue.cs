using System;
using OC.Core.Operations;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        [HideInInspector] public DialogueOption[] DialogueOptions;
        [HideInInspector] public Action<int> OnDialogueOptionSelected;


        [HideInInspector] public bool inDialogue = false;
        
        private bool _waitForDialogueOption;

        public bool WaitForDialogueOption
        {
            get => _waitForDialogueOption;
            set
            {
                if (_waitForDialogueOption != value)
                {
                    _waitForDialogueOption = value;
                    OnTextChanged();
                }
            }
        }
        
        public void ReleaseDialogue()
        {
            inDialogue = false;
            WaitForDialogueOption = false;
            DialogueOptions = null;
            OnDialogueOptionSelected = null;
        }

        public void SelectDialogueOption(int idx)
        {
            WriteLine($"(你选择了[{DialogueOptions[idx].Line.RawText}])");
            _lastAction = (string)DialogueOptions[idx].Line.RawText.Clone();
            OnDialogueOptionSelected?.Invoke(idx);
            OnDialogueOptionSelected = null;
            WaitForDialogueOption = false;
            DialogueOptions = null;
            // ReleaseDialogue();
        }
        
        public string DialogueOptionText(DialogueOption[] options)
        {
            if (options.Length == 0)
            {
                return "NULL OPTIONS";
            }

            var str = "";

            str += "\n\n=====[对话]===========================\n";

            for (int i = 0; i < options.Length; i++)
            {
                if (i == HighlightOptionId)
                {
                    str +=
                        $"<color=#00FF00><link=\"{i}\">{i + 1}. {options[i].Line.RawText}</link></color>\t\t";
                }
                else
                {
                    str += $"<link=\"{i}\">{i + 1}. {options[i].Line.RawText}</link>\t\t";
                }

                if (i % 4 == 4 - 1) str += "\n";
            }

            return str;
        }

        
        
    }
}