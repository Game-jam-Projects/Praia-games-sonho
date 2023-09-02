using DreamTeam.Runtime.System.Core;

namespace DreamTeam.Runtime.System.UI
{
    public class ResumePauseButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            GameManager.Instance.ResumeGame();
        }
    }
}