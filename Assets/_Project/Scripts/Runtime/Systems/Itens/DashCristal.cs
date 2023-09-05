using DreamTeam.Runtime.Systems.Core;
using UnityEngine;
using System.Collections;

public class DashCristal : MonoBehaviour, ICollectible
{
    public float reloadTime;    
    private bool isCollected;
    public GameObject spriteRenderer;
    public void Collect()
    {
        if(isCollected == true) { return; }
        CoreSingleton.Instance.playerController.DashCrystal();
        spriteRenderer.SetActive(false);
        if (reloadTime > 0)
        {
            StartCoroutine(nameof(IERespawn));
        }
    }

    IEnumerator IERespawn()
    {
        yield return new WaitForSeconds(reloadTime);
        spriteRenderer.SetActive(true);
        isCollected = false;
    }
}
