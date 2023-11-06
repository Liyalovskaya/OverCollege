using System;
using System.Collections.Generic;
using OC.Core;
using OC.Core.Operations;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using DialogueOption = OC.Core.Operations.DialogueOption;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        public TextMeshProUGUI mainTextUGUI;
        
        private int _highlightOptionId = -1;

        public int HighlightOptionId
        {
            get => _highlightOptionId;
            set
            {
                if (_highlightOptionId == value) return;
                _highlightOptionId = value;
                UpdateViewer();
            }
        }
    }
}