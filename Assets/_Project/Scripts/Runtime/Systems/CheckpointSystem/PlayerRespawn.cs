using DreamTeam.Runtime.System.Core;
using DreamTeam.Runtime.Systems.CheckpointSystem;
using DreamTeam.Runtime.Systems.Health;
using EasyTransition;
using UnityEngine;

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
    }

    private void RespawnPlayerByDeath(IDamageable damageable)
    {
        var spawnPosition = CheckpointManager.Instance.GetCheckpointPosition();

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        GameManager.Instance.CanPause = false;

        TransitionManager.Instance().Transition(transition, playerAnimationTime);
        TransitionManager.Instance().onTransitionCutPointReached += () =>
        {
            transform.position = spawnPosition;
            GameManager.Instance.CanPause = true;
            healthSystem.ResetLife();

            animator.updateMode = AnimatorUpdateMode.Normal;
        };
    }

    [ContextMenu("Respawn")]
    public void ForceRespawn()
    {
        healthSystem.TakeDamage(Vector3.zero, 1f);
    }
}
