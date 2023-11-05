

namespace OC.Core
{
    public interface IGameRunTextTrigger
    {
        void OnLocationMove();
        void OnMoneyChanged();
        void OnTextChanged();

        void OnTimeChanged();

        void RunDialogue(string id);

        public void SelectDialogueOption(int idx);
    }
}