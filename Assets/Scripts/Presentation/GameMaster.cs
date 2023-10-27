using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using OC.ConfigData;
using OC.Base;
using OC.Core;
using OC.Core.Locations;
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
            DisplayLocationMain();
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
        }
        
        private void Update()
        {
            HighlightOptionIdx =
                TMP_TextUtilities.FindIntersectingLink(mainTextUGUI, Input.mousePosition, Camera.main);
            // Debug.Log(HighlightOptionIdx);
            if (HighlightOptionIdx != -1)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (DialogueOptions != null)
                    {
                        SelectDialogueOption(HighlightOptionIdx);
                    }

                    if (Operations.Count != 0)
                    {
                        SelectOperation(HighlightOptionIdx);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Manually Refresh Text");
                OnTextChanged();
            }
        }
    }
}