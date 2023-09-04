using System.Collections.Generic;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.InteractionSystem
{
    public class Lever : InteractableBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite disabled;
        [SerializeField] private Sprite enable;

        [SerializeField] private List<Door> doorToOpen;

        private bool isTriggered;

        private void Start()
        {
            _spriteRenderer.sprite = disabled;
        }

        public override bool Interact()
        {
            if (isTriggered)
                return false;

            isTriggered = true;
            interactCollider.enabled = false;
            _spriteRenderer.sprite = enable;
            
            
            doorToOpen.ForEach(x => x.OpenDoor());
            
            return true;
        }
    }
}