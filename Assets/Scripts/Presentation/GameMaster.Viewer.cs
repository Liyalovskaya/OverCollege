using System;
using System.Collections.Generic;
using OC.Core;
using OC.Core.Operations;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        private string _lastAction = "";
        
        public TextMeshProUGUI mainTextUGUI;
        
        private int _highlightOptionId = -1;

        public int HighlightOptionId
        {
            get => _highlightOptionId;
            set
            {
                if (_highlightOptionId == value) return;
                _highlightOptionId = value;
                OnTextChanged();
            }
        }

        private string _mainText;

        public string MainText
        {
            get => _mainText;
            set
            {
                _mainText = value;
                OnTextChanged();
            }
        }
        
        
        public void ClearText(bool hintLastAction = true)
        {
            MainText = "";
            if (hintLastAction)
            {
                MainText += $"(上次的选择是[{_lastAction}])\n";
            }
        }

        public void WriteLine(string str)
        {
            if (MainText == null) MainText = "";
            MainText += str;
            MainText += '\n';
        }
    }
}