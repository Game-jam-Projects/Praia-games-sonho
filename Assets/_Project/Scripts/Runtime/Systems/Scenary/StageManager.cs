using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamTeam.Runtime.Systems.Core;

public class StageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CoreSingleton.Instance.gameManager.chrono.Start();
    }

   
}