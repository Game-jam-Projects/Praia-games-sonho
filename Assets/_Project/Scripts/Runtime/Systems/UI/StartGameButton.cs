using DreamTeam.Runtime.System.Core;

namespace DreamTeam.Runtime.System.UI
{
    public class StartGameButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            SceneLoader.Instance.NextScene();
        }
    }
}