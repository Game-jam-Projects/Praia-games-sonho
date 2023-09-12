using EasyTransition;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DreamTeam.Runtime.Systems.Core
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        public event Action OnSceneStartLoading;

        [SerializeField] private int titleScene = 2;
        [SerializeField] private TransitionSettings transition;
        [SerializeField] private float animationFadeTime = 0.3f;

        private bool isLoadingScene;

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                nextSceneIndex = titleScene;

            LoadScene(nextSceneIndex);
        }

        public void LoadTitle()
        {
            LoadScene(titleScene);
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

            TransitionManager.Instance().onTransitionCutPointReached += () =>
            {
                GameManager.Instance.ForcedResumeGame();
                isLoadingScene = false;
            };

            yield return new WaitForEndOfFrame();
        }
    }
}