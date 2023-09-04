using DreamTeam.Runtime.System.Core;
using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public class PassNextScene : MonoBehaviour
    {
        [SerializeField] private PassGameObject passGame;
        [SerializeField] private AutoTimer timer;

        private void Start()
        {
            passGame.OnFinishedCutscene += PassGame_OnFinishedCutscene;
        }

        private void OnDestroy()
        {
            passGame.OnFinishedCutscene -= PassGame_OnFinishedCutscene;
        }

        private void PassGame_OnFinishedCutscene()
        {
            timer.Start(timer.InitTime);
            timer.OnExpire += () =>
            {
                SceneLoader.Instance.NextScene();
            };
        }
    }
}