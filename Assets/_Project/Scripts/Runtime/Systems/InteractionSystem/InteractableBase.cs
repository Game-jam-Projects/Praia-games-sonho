using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] protected Collider2D interactCollider;

        public abstract bool Interact();
    }
}