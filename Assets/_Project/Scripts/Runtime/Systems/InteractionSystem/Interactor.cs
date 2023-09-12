using DreamTeam.Runtime.Systems.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;

        [SerializeField] private Transform checkInteract;
        [SerializeField] private Vector2 checkSize;
        [SerializeField] private LayerMask whatIsInteractable;

        private Collider2D cachedCollider;
        private InteractableBase currentInteractable;

        public bool canInteract = true;

        private void Start()
        {
            inputReader.OnInteractDown += TryInteract;
        }

        private void OnDestroy()
        {
            inputReader.OnInteractDown -= TryInteract;
        }

        private void FixedUpdate()
        {
            var result = Physics2D.OverlapBox(checkInteract.position, checkSize, 0f, whatIsInteractable);

            if (result)
            {
                if (cachedCollider != result)
                {
                    if (result.TryGetComponent(out InteractableBase interactable))
                    {
                        cachedCollider = result;
                        currentInteractable = interactable;
                    }
                }
            }
            else
            {
                cachedCollider = null;
                currentInteractable = null;
            }
        }

        private void TryInteract()
        {
            if (!currentInteractable)
                return;

            if (!canInteract)
                return;

            if (GameManager.Instance && GameManager.Instance.Paused)
                return;

            currentInteractable.Interact();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(checkInteract.position, checkSize);
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.TryGetComponent(out InteractableBase interactable))
        //    {
        //        currentInteractable = interactable;
        //    }
        //}

        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.TryGetComponent(out InteractableBase interactable))
        //    {
        //        currentInteractable = null;
        //    }
        //}
    }
}