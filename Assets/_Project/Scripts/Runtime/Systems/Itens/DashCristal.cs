using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class DashCristal : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        CoreSingleton.Instance.playerController.NewDash();
        Destroy(this.gameObject);
    }
}
