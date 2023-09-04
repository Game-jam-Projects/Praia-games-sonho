using DG.Tweening;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite closed;
        [SerializeField] private Sprite open;

        [SerializeField] private BoxCollider2D collider2d;

        private void Start()
        {
            _spriteRenderer.sprite = closed;
            collider2d.enabled = true;
        }

        [ContextMenu("OpenDoor")]
        public void OpenDoor()
        {
            _spriteRenderer.sprite = open;
            collider2d.enabled = false;
        }
    }
}