using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.CheckpointSystem
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        private Vector3 currentCheckpointPosition = Vector3.zero;

        private Checkpoint lastCheckpoint;

        public void SetCheckpoint(Checkpoint newCheckpoint, Vector3 checkpointSpawnPosition)
        {
            if (lastCheckpoint)
            {
                lastCheckpoint.DownFlag();
            }

            lastCheckpoint = newCheckpoint;
            currentCheckpointPosition = checkpointSpawnPosition;
        }

        public Vector3 GetCheckpointPosition()
        {
            return currentCheckpointPosition;
        }

        public void ResetCheckpoint()
        {
            lastCheckpoint = null;
            currentCheckpointPosition = Vector3.zero;
        }
    }
}