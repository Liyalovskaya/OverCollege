using System;
using OC.Core.Operations;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        // [HideInInspector] public DialogueOption[] DialogueOptions;
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


        
        
    }
}