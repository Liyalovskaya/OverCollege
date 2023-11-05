using System.Collections.Generic;
using OC.ConfigData;
using OC.Core.Operations;
using UnityEngine;

namespace OC.Core
{
    public class OcScene
    {
        public readonly List<Operation> Operations = new List<Operation>();
        public string MainText = "";
        public string OperationText = "";
        public virtual void RefreshOperations()
        {
            Operations.Clear();
            OperationText = "";
        }

        public virtual void Enter()
        {
            
        }


        public void Write(string content)
        {
            MainText += content;
        }

        public void WriteLine(string content)
        {
            Write($"{content}\n");
        }
        
        

    }
}
