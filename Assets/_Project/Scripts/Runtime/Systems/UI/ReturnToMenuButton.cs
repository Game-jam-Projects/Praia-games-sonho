using DreamTeam.Runtime.System.Core;
using DreamTeam.Runtime.Systems.Core;

namespace DreamTeam.Runtime.System.UI
{
    public class ReturnToMenuButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            GameManager.Instance.RankingClear();

            CoreSingleton.Instance.gameStateManager.ChangeStageType(StageType.Dream);
            SceneLoader.Instance.LoadTitle();
        }
    }
}