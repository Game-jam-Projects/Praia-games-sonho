using System;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public class PassGameObject : MonoBehaviour
    {
        public event Action OnFinishedCutscene;

        [SerializeField] private GameObject[] gameObjects;
        private int index;
        private bool finished;

        private void Start()
        {
            UpdateHandle();
        }

        public void Next()
        {
            index++;

            if (index == gameObjects.Length && finished == false)
            {
                index = gameObjects.Length;

                finished = true;

                OnFinishedCutscene?.Invoke();
                return;
            }

            UpdateHandle();
        }

        public void UpdateHandle()
        {
            foreach (GameObject go in gameObjects)
            {
                go.SetActive(false);
            }

            gameObjects[index].SetActive(true);
        }
    }
}