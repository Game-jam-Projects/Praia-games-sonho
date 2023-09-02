using EasyTransition;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DreamTeam.Runtime.System.Core
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public event Action OnSceneStartLoading;

        [SerializeField] private TransitionSettings transition;
        [SerializeField] private float animationFadeTime = 0.3f;

        private bool isLoadingScene;

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadTitle()
        {
            LoadScene(1);
        }

        private void LoadScene(int sceneIndex)
        {
            if (isLoadingScene)
                return;

            StartCoroutine(LoadSceneRoutine(sceneIndex));
        }

        private IEnumerator LoadSceneRoutine(int sceneIndex)
        {
            isLoadingScene = true;

            OnSceneStartLoading?.Invoke();

            TransitionManager.Instance().Transition(sceneIndex, transition, 0f);

            GameManager.Instance.ResumeGame();

            yield return new WaitForEndOfFrame();

            isLoadingScene = false;
        }
    }
}