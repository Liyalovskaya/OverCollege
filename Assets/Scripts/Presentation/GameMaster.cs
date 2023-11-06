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
using DialogueOption = Yarn.Unity.DialogueOption;

namespace OC.Presentation
{
    public partial class GameMaster : Singleton<GameMaster>
    {
        public static DialogueRunner DialogueRunner;
        public static DialogueViewer DialogueViewer;

        public static GameRun GameRun { get; set; }


        private void Awake()
        {
            _ = StartAsync();
        }

        private async UniTask StartAsync()
        {
            await InitializeRestAsync();
            
            GameRun = new GameRun();
            GameRun.TextTrigger = this;
            
            InitEntities();
            LocationScene = new LocationScene(GameRun);
            DialogueScene = new DialogueScene(GameRun);
            GameRun.CurrentLocation = GameRun.Locations.FirstOrDefault(x => x.Id == "School_Hall");
            CurrentScene = LocationScene;
            
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
                location.EnterGameRun(GameRun);
            }

            foreach (var location in GameRun.Locations)
            {
                foreach (var id in location.Config.Connections)
                {
                    location.Connections.Add(GameRun.Locations.FirstOrDefault(x => x.Id == id));
                }
            }

            foreach (var config in CharacterConfig.AllConfig())
            {
                var character = Library.CreateCharacter(config.Id);
                character.EnterGameRun(GameRun);
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
                    CurrentScene.SelectOption(HighlightOptionId);
                }
            }
            else
            {
                HighlightOptionId = -1;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Manually Refresh Text");
                UpdateViewer();
            }
        }
    }
}