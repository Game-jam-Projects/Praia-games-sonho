using DreamTeam.Runtime.Systems.Core;

namespace DreamTeam.Runtime.Systems.UI
{
    public class StartGameButton : ButtonBase
    {
        protected override void ButtonBehaviour()
        {
            SceneLoader.Instance.NextScene();
        }
    }
}