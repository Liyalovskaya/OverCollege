

namespace OC.Core
{
    public interface IGameRunTextTrigger
    {
        public void OnLocationMove();
        public void OnMoneyChanged();

        public void OnTimeChanged();

        public void RunDialogue(string id);

        public void OnCharacterMove();

        public void UpdateViewer();

    }
}