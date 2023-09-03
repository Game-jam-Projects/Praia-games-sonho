using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamTeam.Runtime.Systems.Core;

public class SecretPassage : MonoBehaviour, IBreakObjects
{
    public List<StageType> breakableIn;

    public void BreakObject()
    {
        if(breakableIn.Contains(CoreSingleton.Instance.gameStateManager.GetStageType()))
        {
            print("Som de Parede sendo Quebrada");
            Destroy(this.gameObject);
        }
    }
}
