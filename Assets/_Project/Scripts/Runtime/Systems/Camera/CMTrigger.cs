using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMTrigger : MonoBehaviour
{
    public GameObject eneableCam;
    public GameObject disableCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            eneableCam.SetActive(true);
            disableCam.SetActive(false);
        }
    }
}
