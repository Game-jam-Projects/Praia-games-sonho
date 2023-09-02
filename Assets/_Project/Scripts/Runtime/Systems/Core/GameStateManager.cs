using UnityEngine;

namespace DreamTeam.Runtime.Systems.Core
{
    public enum StageType
    {
        Dream, Nightmare
    }
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private StageType stageType;

        public delegate void ChangeType(StageType stageType);
        public event ChangeType ChagedStageType;

        public void ChangeStageType(StageType stageType)
        {
            this.stageType = stageType;

            ChagedStageType?.Invoke(this.stageType);
        }

        public StageType GetStageType()
        {
            return stageType;
        }

        public void ChangeStageType()
        {
            switch (stageType)
            {
                case StageType.Dream:
                    ChangeStageType(StageType.Nightmare);
                    break;

                case StageType.Nightmare:
                    ChangeStageType(StageType.Dream);
                    break;
            }
        }

    }
}
