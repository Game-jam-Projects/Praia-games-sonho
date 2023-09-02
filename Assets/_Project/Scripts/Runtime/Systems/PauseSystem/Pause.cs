using DreamTeam.Runtime.System.Core;
using UnityEngine;

namespace DreamTeam.Runtime.System.PauseSystem
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