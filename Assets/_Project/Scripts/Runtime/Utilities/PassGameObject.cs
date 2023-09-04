using DG.Tweening;
using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
using System;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public class PassGameObject : MonoBehaviour
    {
        public event Action OnFinishedCutscene;

        [SerializeField] private CanvasGroup[] gameObjects;
        [SerializeField] private float duration;
        [SerializeField] private bool disableOnFinish;

        private int index;
        private bool finished;

        [SerializeField] private AutoTimer timer;

        [SerializeField] private InputReader cutsceneInputReader;

        private void Start()
        {
            cutsceneInputReader.EnableCutsceneInput();

            UpdateHandle(true);

            timer.Start(timer.InitTime, true);
            timer.OnExpire += () =>
            {
                Next();
            };

            cutsceneInputReader.OnSkipCutsceneDown += Next;
        }

        private void OnDestroy()
        {
            cutsceneInputReader.OnSkipCutsceneDown -= Next;

            cutsceneInputReader.DisableCutsceneInput();
        }

        public void Next()
        {
            if(finished) 
                return;

            index++;
            timer.Restart();

            if (index == gameObjects.Length)
            {
                finished = true;

                if(disableOnFinish)
                {
                    foreach (var go in gameObjects)
                    {
                        go.DOFade(0f, duration);
                    }
                }

                OnFinishedCutscene?.Invoke();
                return;
            }

            UpdateHandle(false);
        }

        public void UpdateHandle(bool forced)
        {
            foreach (var go in gameObjects)
            {
                go.DOFade(0f, forced ? 0f : duration);
            }

            gameObjects[index].DOFade(1f, forced ? 0f : duration);
        }
    }
}