using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamTeam.Runtime.Systems.Core;


public class TradeDream : MonoBehaviour, ICollectible
{
    public GameObject renderer;
    private bool isCollected;
    public void Collect()
    {
     if(isCollected == false)
        {
            isCollected = true;
            renderer.SetActive(false);
            CoreSingleton.Instance.gameManager.SetItem(1);
            
        }
    }

}
