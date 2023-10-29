using System;
using System.Collections.Generic;
using Yarn.Unity;

namespace OC.Core
{
    public class Operation
    {
        public string Id;
        public string DialogueId = "";
        public string Tag;
        public bool NewLine = false;
        public object Target;


        public Operation(object target)
        {
            Target = target;
        }

        public virtual string Content()
        {
            return Id;
        }

        public virtual void Execute(GameRun gameRun)
        {

        }
        
    }
}

