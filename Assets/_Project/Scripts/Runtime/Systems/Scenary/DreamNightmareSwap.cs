using DG.Tweening;
using DreamTeam.Runtime.Systems.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Scenary
{
    [Serializable]
    public struct Swap
    {
        public List<SpriteRenderer> renderers;
        public List<Collider2D> colliders;
        public GameObject gameObject;
    }

    public class DreamNightmareSwap : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;

        [SerializeField] private Swap dream;
        [SerializeField] private Swap nightmare;

        private void Start()
        {
            CoreSingleton.Instance.gameStateManager.ChagedStageType += GameStateManager_ChagedStageType;

            ForcedChagedStageType(CoreSingleton.Instance.gameStateManager.GetStageType());
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
                    OnDream(false);
                    break;

                case StageType.Nightmare:
                    OnNightmare(false);
                    break;
            }
        }

        private void ForcedChagedStageType(StageType stageType)
        {
            switch (stageType)
            {
                case StageType.Dream:
                    OnDream(true);
                    break;

                case StageType.Nightmare:
                    OnNightmare(true);
                    break;
            }
        }

        private void OnDream(bool forced)
        {
            DisableObjects(nightmare, forced);
            EnableObjects(dream, forced);
        }

        private void OnNightmare(bool forced)
        {
            DisableObjects(dream, forced);
            EnableObjects(nightmare, forced);
        }

        private void DisableObjects(Swap swap, bool forced)
        {
            swap.colliders.ForEach(collider => collider.enabled = false);

            foreach (var renderer in swap.renderers)
            {
                if (swap.renderers.Last() == renderer)
                {
                    renderer.DOFade(0f, forced ? 0f : transitionDuration).OnComplete(() =>
                    {
                        if(swap.gameObject)
                            swap.gameObject.SetActive(false);
                    });
                }
                else
                {
                    renderer.DOFade(0f, forced ? 0f : transitionDuration);
                }
            }
        }

        private void EnableObjects(Swap swap, bool forced)
        {
            if (swap.gameObject)
                swap.gameObject.SetActive(true);
            swap.colliders.ForEach(collider => collider.enabled = true);

            foreach (var renderer in swap.renderers)
            {
                if (swap.renderers.Last() == renderer)
                {
                    renderer.DOFade(1f, forced ? 0f : transitionDuration).OnComplete(() =>
                    {
                        //caso precise ativar os colisores, coloque a linha 84 aqui
                    });
                }
                else
                {
                    renderer.DOFade(1f, forced ? 0f : transitionDuration);
                }
            }
        }
    }
}