using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public class FadeCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float duration;

        [SerializeField] private float delay;

        private void Start()
        {
            canvasGroup.gameObject.SetActive(true);

            StartCoroutine(DelayedStart());
        }

        private IEnumerator DelayedStart()
        {
            yield return new WaitForSeconds(delay);
            canvasGroup.DOFade(0, duration);
        }
    }
}