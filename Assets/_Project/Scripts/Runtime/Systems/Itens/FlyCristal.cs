using System.Collections;
using DreamTeam.Runtime.Systems.Core;
using System.Collections.Generic;
using UnityEngine;

public class FlyCristal : MonoBehaviour, ICollectible
{
    public float reloadTime;
    public float flightTimeDreamMode;
    public float flightTimeNightmareMode;
    private bool isCollected;
    public GameObject spriteRenderer;

    public Transform startDirection;

    private bool isStarted;

    void Start()
    {
        if(isStarted == false)
        {
            isStarted = true;
            CoreSingleton.Instance.gameManager.OnTransitionFinished += Reset;
        }
    }

    private void OnEnable()
    {
        if(isStarted == true)
        {
            CoreSingleton.Instance.gameManager.OnTransitionFinished += Reset;
        }
    }

    private void OnDisable()
    {
        CoreSingleton.Instance.gameManager.OnTransitionFinished -= Reset;
    }

    public void Collect()
    {
        if (isCollected == true) { return; }
        isCollected = true;
        switch(CoreSingleton.Instance.gameStateManager.GetStageType())
        {
            case StageType.Dream:
                CoreSingleton.Instance.playerController.Fly(startDirection.eulerAngles, flightTimeDreamMode);
                break;

            case StageType.Nightmare:
                CoreSingleton.Instance.playerController.Fly(startDirection.eulerAngles, flightTimeNightmareMode);
                break;
        }
        
        spriteRenderer.SetActive(false);
        StartCoroutine(nameof(IERespawn));
    }

    IEnumerator IERespawn()
    {
        yield return new WaitForSeconds(reloadTime);
        spriteRenderer.SetActive(true);
        isCollected = false;
    }

    private void Reset()
    {
        spriteRenderer.SetActive(true);
        isCollected = false;
        StopCoroutine(nameof(IERespawn));
    }
}
