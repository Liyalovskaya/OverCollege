using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OC.ConfigData;
using OC.Base;
using OC.Core;
using OC.Core.Locations;
using OC.Core.Operations;
using OC.Core.Scenes;
using TMPro;
using Yarn.Unity;
using UnityEngine;

namespace OC.Presentation
{
    public partial class GameMaster : Singleton<GameMaster>
    {
        public static DialogueRunner DialogueRunner;
        public static DialogueViewer DialogueViewer;

        public static GameRun GameRun;

        public static List<Operation> Operations = new();

        public OcScene Scene;
        

        private void Awake()
        {
            _ = StartAsync();
        }

        private async UniTask StartAsync()
        {
            await InitializeRestAsync();
            
            GameRun = new GameRun();
            GameRun.TextTrigger = this;

            Operations = new List<Operation>();

            InitEntities();
            GameRun.CurrentLocation = GameRun.Locations.FirstOrDefault(x => x.Id == "School_Hall");

            OnTimeChanged();

            // DisplayLocationMain();
            LeftPanel.Instance.InfoRefresh();
        }

        public async UniTask InitializeRestAsync()
        {
            ConfigDataManager.Initialize();
            await Library.RegisterAllAsync();
            DialogueRunner = GetComponent<DialogueRunner>();
            DialogueViewer = GetComponent<DialogueViewer>();
        }

        public void InitEntities()
        {
            foreach (var config in LocationConfig.AllConfig())
            {
                var location = Library.CreateLocation(config.Id);
                GameRun.Locations.Add(location);
            }

            foreach (var location in GameRun.Locations)
            {
                foreach (var id in location.Config.Connections)
                {
                    location.Connections.Add(GameRun.Locations.FirstOrDefault(x => x.Id == id));
                    // Debug.Log($"{location.Id} connect to {id}");
                }
            }

            foreach (var config in CharacterConfig.AllConfig())
            {
                var character = Library.CreateCharacter(config.Id);
                GameRun.Characters.Add(character);
            }
        }

        private void Update()
        {
            var idx =
                TMP_TextUtilities.FindIntersectingLink(mainTextUGUI, Input.mousePosition, Camera.main);
            if (idx != -1)
            {
                mainTextUGUI.textInfo.linkInfo[idx].GetLinkText();
                int.TryParse(mainTextUGUI.textInfo.linkInfo[idx].GetLinkID(), out var id);
                HighlightOptionId = id;
                // Debug.Log(HighlightOptionIdx);

                if (Input.GetMouseButtonUp(0))
                {
                    if (WaitForDialogueOption)
                    {
                        SelectDialogueOption(HighlightOptionId);
                    }

                    if (WaitForOperation)
                    {
                        SelectOperation(HighlightOptionId);
                    }
                }
            }
            else
            {
                HighlightOptionId = -1;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Manually Refresh Text");
                OnTextChanged();
            }
        }
    }
}