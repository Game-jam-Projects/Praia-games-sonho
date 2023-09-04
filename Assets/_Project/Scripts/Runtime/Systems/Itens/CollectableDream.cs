using System.Collections;
using System.Collections.Generic;
using DreamTeam.Runtime.System.Core;
using UnityEngine;

public class CollectableDream : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.SetCollectableDreams();
    }

}
