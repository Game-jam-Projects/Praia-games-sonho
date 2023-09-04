using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public class Lever : InteractableBase
    {
        [SerializeField] private Door doorToOpen;

        private bool isTriggered;

        public override bool Interact()
        {
            if (isTriggered)
                return false;

            isTriggered = true;
            interactCollider.enabled = false;

            doorToOpen.OpenDoor();
            return true;
        }
    }
}