using System.Collections.Generic;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.Core
{
    public class CollectiblesManager : MonoBehaviour
    {
        public int totalCollectableDream;

        public List<CollectableDream> preCollected = new List<CollectableDream>();
        public List<CollectableDream> collected = new List<CollectableDream>();

        private void Start()
        {
            CoreSingleton.Instance.gameManager.OnTransitionFinished += ResetItens;
        }

        private void OnDestroy()
        {
            CoreSingleton.Instance.gameManager.OnTransitionFinished -= ResetItens;
        }

        public void PreCollect(CollectableDream item)
        {
            if (preCollected.Contains(item) == false)
            {
                preCollected.Add(item);
            }
        }

        public void ConfirmedCollection()
        {
            foreach (CollectableDream item in preCollected)
            {
                item.Collected();
                collected.Add(item);
            }

            preCollected.Clear();
        }

        private void ResetItens()
        {
            preCollected.Clear();
        }

    }
}
