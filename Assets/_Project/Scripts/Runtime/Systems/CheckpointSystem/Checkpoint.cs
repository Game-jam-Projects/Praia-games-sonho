using System;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.CheckpointSystem
{
    public class Checkpoint : MonoBehaviour
    {
        private Animator animator;

        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private bool initCheckpoint;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if(initCheckpoint)
            {
               UpdateCheckpoint();
            }
        }

        public void DownFlag()
        {
            animator.SetTrigger("Down");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
            {
                if (CheckpointManager.Instance.GetCheckpointPosition() == playerSpawnPoint.position)
                {
                    return;
                }

                UpdateCheckpoint();
            }
        }

        private void UpdateCheckpoint()
        {
            CheckpointManager.Instance.SetCheckpoint(this, playerSpawnPoint.position);
            animator.SetTrigger("Up");
        }
    }
}