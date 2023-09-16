using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Systems.Health;
using DreamTeam.Runtime.Systems.Core;
using EasyTransition;
using UnityEngine;

namespace DreamTeam.Runtime.Systems.CheckpointSystem
{
    public class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private TransitionSettings transition;
        [SerializeField] private float playerAnimationTime = 1f;

        private HealthSystem healthSystem;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            healthSystem = GetComponent<HealthSystem>();
        }

        private void Start()
        {
            healthSystem.OnDie += RespawnPlayerByDeath;
        }

        private void OnDestroy()
        {
            healthSystem.OnDie -= RespawnPlayerByDeath;
            TransitionManager.Instance().onTransitionCutPointReached -= () => ResetPlayer(Vector3.zero);
        }

        private void RespawnPlayerByDeath(IDamageable damageable)
        {
            var spawnPosition = CheckpointManager.Instance.GetCheckpointPosition();

            animator.updateMode = AnimatorUpdateMode.UnscaledTime;

            GameManager.Instance.CanPause = false;

            TransitionManager.Instance().Transition(transition, playerAnimationTime);
            TransitionManager.Instance().onTransitionCutPointReached += () => ResetPlayer(spawnPosition);
            
        }

        private void ResetPlayer(Vector3 spawnPosition)
        {
            if (transform == null) { return; }
            transform.position = spawnPosition;
            GameManager.Instance.CanPause = true;
            GameManager.Instance.TriggerTransitionFinish();
            CoreSingleton.Instance.camerasManager.Respawn();
            healthSystem.ResetLife();

            animator.updateMode = AnimatorUpdateMode.Normal;
        }

        [ContextMenu("Respawn")]
        public void ForceRespawn()
        {
            healthSystem.TakeDamage(Vector3.zero, 1f);
        }
    }
}