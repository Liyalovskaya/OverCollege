using System;
using OC.Core;
using OC.Core.Operations;
using OC.Core.Scenes;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

namespace OC.Presentation
{
    public partial class GameMaster
    {
        public LocationScene LocationScene { get; set; }
        public DialogueScene DialogueScene { get; set; }

        private OcScene _currentScene;
        public OcScene CurrentScene
        {
            get => _currentScene;
            set
            {
                if (_currentScene == value || value == null) return;
                _currentScene = value;
                _currentScene.Update();
                UpdateViewer();
            }
        }
    }
}