using DreamTeam.Runtime.Systems.Core;
using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.SceneLoaderSystem
{
    public class AutoNextScene : MonoBehaviour
    {
        [SerializeField] private float delay;

        private AutoTimer startTime = new();

        private void Start()
        {
            startTime.Start(delay);
            startTime.OnExpire += NextScene;
        }

        private void NextScene()
        {
            SceneLoader.Instance.NextScene();
        }
    }
}