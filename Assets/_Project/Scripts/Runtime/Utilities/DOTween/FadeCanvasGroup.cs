using DG.Tweening;
using UnityEngine;

namespace DreamTeam.Runtime.Utilities
{
    public class FadeCanvasGroup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float duration;

        private void Start()
        {
            canvasGroup.gameObject.SetActive(true);

            canvasGroup.DOFade(0, duration);
        }
    }
}