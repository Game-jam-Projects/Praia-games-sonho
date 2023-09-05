using System.Collections;
using DreamTeam.Runtime.Systems.Core;
using System.Collections.Generic;
using UnityEngine;

public class FlyCristal : MonoBehaviour, ICollectible
{
    public float reloadTime;
    public float flightTime;
    private bool isCollected;
    public GameObject spriteRenderer;

    public Transform startDirection;
    
    public void Collect()
    {
        if (isCollected == true) { return; }
        isCollected = true;
        CoreSingleton.Instance.playerController.Fly(startDirection.eulerAngles, flightTime);
        spriteRenderer.SetActive(false);
        StartCoroutine(nameof(IERespawn));
    }

    IEnumerator IERespawn()
    {
        yield return new WaitForSeconds(reloadTime);
        spriteRenderer.SetActive(true);
        isCollected = false;
    }
}
