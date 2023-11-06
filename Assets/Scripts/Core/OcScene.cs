using System.Collections.Generic;
using OC.ConfigData;
using OC.Core.Operations;
using UnityEngine;

namespace OC.Core
{
    public class OcScene
    {
        public GameRun GameRun { get; set; }
        public readonly List<Operation> Operations = new List<Operation>();
        public string MainText = "";

        public OcScene(GameRun gameRun)
        {
            GameRun = gameRun;
        }

        public virtual void Update()
        {
            
        }

        public virtual void SelectOption(int idx)
        {
            GameRun.LastOperationContent = Operations[idx].Content();
            Operations[idx].Execute(GameRun);
        }


        public void Write(string content)
        {
            MainText += content;
        }

        public void WriteLine(string content)
        {
            Write($"{content}\n");
        }

        public void ReleaseText()
        {
            MainText = "";
        }

        public virtual string OperationText(int highlightIdx = -1)
        {
            return "";
        }

        protected void ShowLastOperation()
        {
            if (!string.IsNullOrEmpty(GameRun.LastOperationContent))
            {
                WriteLine($"(上一次的选择:[{GameRun.LastOperationContent}])");
            }
        }
        

    }
}
