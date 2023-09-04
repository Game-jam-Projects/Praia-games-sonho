using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private Transform checkInteract;
    [SerializeField] private Vector2 checkSize;
    [SerializeField] private LayerMask whatIsBoss;


    private Collider2D cachedCollider;
    private Boss currentBoss;

    private void FixedUpdate()
    {
        var result = Physics2D.OverlapBox(checkInteract.position, checkSize, 0f, whatIsBoss);

        if (result)
        {
            if (cachedCollider != result)
            {
                if (result.TryGetComponent(out Boss boss))
                {
                    cachedCollider = result;
                    currentBoss = boss;

                    boss.SetStop();
                }
            }
        }
        else
        {
            if (currentBoss)
            {
                currentBoss.SetChase();
            }

            cachedCollider = null;
            currentBoss = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(checkInteract.transform.position, checkSize);
    }
}
