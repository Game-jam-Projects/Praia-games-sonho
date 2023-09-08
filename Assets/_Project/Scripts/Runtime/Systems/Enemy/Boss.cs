using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float chasingSpeed;
    [SerializeField] private bool isLookLeft;
    private Transform target;
    private Animator animator;

    private bool isLocked;
    private bool isFirstView;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += GameStateManager_ChagedStageType;
        CoreSingleton.Instance.gameManager.OnTransitionFinished += SetNewPosition;
    }

    private void Update()
    {
        if(isLocked) return;

        if (target)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, target.position, chasingSpeed * Time.deltaTime);

            if (target.position.x < transform.position.x && isLookLeft == false)
            {
                Flip();
            }
            else if (target.position.x > transform.position.x && isLookLeft == true)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1; //Inverte o sinal do scale X
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= GameStateManager_ChagedStageType;
        CoreSingleton.Instance.gameManager.OnTransitionFinished -= SetNewPosition;
    }

    private void GameStateManager_ChagedStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:

                target = null;

                SetStop();

                break;

            case StageType.Nightmare:

                target = CoreSingleton.Instance.playerController.transform;
               isFirstView = true;
                SetChase();

                break;
        }
    }

    public void SetChase()
    {
        isLocked = false;
        animator.SetBool("isHidding", false);
    }

    public void SetStop()
    {
        isLocked = true;
        animator.SetBool("isHidding", true);
    }

    private void SetNewPosition()
    {
        if(isFirstView == false) { return; }

        float multiplier = CoreSingleton.Instance.gameManager.bossDistanceMultiplier;

        Vector3 newPosition = CoreSingleton.Instance.playerController.transform.position;

        if(Random.Range(0,100) <= 50)
        {
            newPosition += new Vector3(15f * multiplier, Random.Range(-12,12) * multiplier, 0);
        }
        else
        {
            newPosition += new Vector3(-15f * multiplier, Random.Range(-12, 12) * multiplier, 0);
        }
        transform.position = newPosition;
    }
}
