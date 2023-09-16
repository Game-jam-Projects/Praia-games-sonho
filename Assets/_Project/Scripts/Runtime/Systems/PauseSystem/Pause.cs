using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.PauseSystem
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;

        private void Start()
        {
            inputReader.OnPauseDown += InputReader_OnPauseDown;
        }

        private void OnDestroy()
        {
            inputReader.OnPauseDown -= InputReader_OnPauseDown;
        }

        private void InputReader_OnPauseDown()
        {
            GameManager.Instance.PauseResume();
        }
    }
}