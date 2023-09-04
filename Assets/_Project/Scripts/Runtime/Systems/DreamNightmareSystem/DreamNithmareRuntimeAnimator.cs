using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class DreamNithmareRuntimeAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private RuntimeAnimatorController dreamController;
    [SerializeField] private RuntimeAnimatorController nightmareController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += GameStateManager_ChagedStageType;
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= GameStateManager_ChagedStageType;
    }

    private void GameStateManager_ChagedStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:
                animator.runtimeAnimatorController = dreamController;
                break;

            case StageType.Nightmare:
                animator.runtimeAnimatorController = nightmareController;
                break;
        }
    }
}
