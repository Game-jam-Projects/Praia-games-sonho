using DreamTeam.Runtime.Systems.Core;

namespace DreamTeam.Runtime.Systems.UI
{
    public class ReloadSceneButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            SceneLoader.Instance.ReloadScene();
        }
    }
}