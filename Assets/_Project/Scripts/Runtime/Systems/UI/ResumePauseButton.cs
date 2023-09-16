using DreamTeam.Runtime.Systems.Core;

namespace DreamTeam.Runtime.Systems.UI
{
    public class ResumePauseButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            GameManager.Instance.ForcedResumeGame();
        }
    }
}