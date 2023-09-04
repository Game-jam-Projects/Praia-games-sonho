using DG.Tweening;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorVisual;
        [SerializeField] private Transform finishPoint;
        [SerializeField] private float duration = 1f;

        [ContextMenu("OpenDoor")]
        public void OpenDoor()
        {
            doorVisual.DOMove(finishPoint.position, duration);
        }
    }
}