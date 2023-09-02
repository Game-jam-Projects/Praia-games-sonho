using DreamTeam.Runtime.System.Core;

namespace DreamTeam.Runtime.System.UI
{
    public class ReloadSceneButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            SceneLoader.Instance.ReloadScene();
        }
    }
}