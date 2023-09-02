using DG.Tweening;
using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Scenary
{
    public struct Swap
    {
        public List<SpriteRenderer> renderers;
        public List<Collider2D> colliders;
        public GameObject gameObject;
    }

    public class DreamNightmare : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;

        [SerializeField] private Swap dream;
        [SerializeField] private Swap nightmare;

        private void Start()
        {
            CoreSingleton.Instance.gameStateManager.ChagedStageType += GameStateManager_ChagedStageType;
        }

        private void OnDestroy()
        {
            CoreSingleton.Instance.gameStateManager.ChagedStageType -= GameStateManager_ChagedStageType;
        }

        private void GameStateManager_ChagedStageType(StageType stageType)
        {
            switch (stageType)
            {
                case StageType.Dream:
                    OnDream();
                    break;

                case StageType.Nightmare:
                    OnNightmare();
                    break;
            }
        }

        private void OnDream()
        {
            DisableObjects(nightmare);
            EnableObjects(dream);
        }

        private void OnNightmare()
        {
            DisableObjects(dream);
            EnableObjects(nightmare);
        }

        private void DisableObjects(Swap swap)
        {
            swap.colliders.ForEach(collider => collider.enabled = false);

            foreach (var renderer in swap.renderers)
            {
                if (swap.renderers.Last() == renderer)
                {
                    renderer.DOFade(0f, transitionDuration).OnComplete(() =>
                    {
                        swap.gameObject.SetActive(false);
                    });
                }
                else
                {
                    renderer.DOFade(0f, transitionDuration);
                }
            }
        }

        private void EnableObjects(Swap swap)
        {
            foreach (var renderer in swap.renderers)
            {
                if (swap.renderers.Last() == renderer)
                {
                    renderer.DOFade(1f, transitionDuration).OnComplete(() =>
                    {
                        swap.gameObject.SetActive(true);
                        swap.colliders.ForEach(collider => collider.enabled = true);
                    });
                }
                else
                {
                    renderer.DOFade(1f, transitionDuration);
                }
            }
        }
    }
}