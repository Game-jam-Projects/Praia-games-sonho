using System.Collections;
using System.Collections.Generic;
using DreamTeam.Runtime.System.Core;
using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class CollectableDream : MonoBehaviour, ICollectible
{

    private bool isCollected;
    private bool isStarted;
    public void Collect()
    {       
        CoreSingleton.Instance.collectiblesManager.PreCollect(this);
        this.gameObject.SetActive(false);
    }

    public void Start()
    {
        if(isStarted == false)
        {
            isStarted = true;
            CoreSingleton.Instance.gameManager.OnTransitionFinished += ResetItem;
        }
    }

    private void OnEnable()
    {
        if(isStarted == true)
        {
            CoreSingleton.Instance.gameManager.OnTransitionFinished += ResetItem;
        }
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameManager.OnTransitionFinished -= ResetItem;
    }

    public void ResetItem()
    {
        if(isCollected == true) { return; }

        this.gameObject.SetActive(true);
    }

    public void Collected()
    {
        isCollected = true;
    }
}
