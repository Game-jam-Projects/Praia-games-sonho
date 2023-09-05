using DreamTeam.Runtime.Systems.Core;
using UnityEngine;
using System.Collections;

public class CMTrigger : MonoBehaviour
{
    public GameObject areaCam;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        CoreSingleton.Instance.camerasManager.RegisterCamera(areaCam);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoreSingleton.Instance.camerasManager.HandleCamera(areaCam);
        }
    }
}
