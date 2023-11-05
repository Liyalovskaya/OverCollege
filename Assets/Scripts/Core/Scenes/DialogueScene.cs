using System.Linq;
using OC.Core.Operations;
using Yarn;
using Yarn.Unity;
using DialogueOption = OC.Core.Operations.DialogueOption;

namespace OC.Core.Scenes
{
    public class DialogueScene : OcScene
    {
        public Dialogue Dialogue { get; set; }
        public Yarn.Unity.DialogueOption[] DialogueOptions { get; set; }

        public DialogueScene(Dialogue dialogue)
        {
            Dialogue = dialogue;
        }

        public override void Enter()
        {
            base.Enter();
        }
        

        public void RunOption(Yarn.Unity.DialogueOption[] dialogueOptions)
        {
            DialogueOptions = dialogueOptions;
            RefreshOperations();
        }


        public override void RefreshOperations()
        {
            base.RefreshOperations();
            for (int i = 0; i < DialogueOptions.Length; i++)
            {
                Operations.Add(new DialogueOption(DialogueOptions[i], i));
            }
        }
        
        
    }
}
