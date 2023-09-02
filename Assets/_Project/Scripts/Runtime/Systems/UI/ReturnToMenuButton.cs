using DreamTeam.Runtime.System.Core;

namespace DreamTeam.Runtime.System.UI
{
    public class ReturnToMenuButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            SceneLoader.Instance.LoadTitle();
        }
    }
}