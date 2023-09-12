using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Systems.CheckpointSystem;
using DreamTeam.Runtime.Systems.Core;

namespace DreamTeam.Runtime.Systems.UI
{
    public class ReturnToMenuButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            GameManager.Instance.RankingClear();
            CheckpointManager.Instance.ResetCheckpoint();
            CoreSingleton.Instance.gameStateManager.ChangeStageType(StageType.Dream);
            SceneLoader.Instance.LoadTitle();
        }
    }
}