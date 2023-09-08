using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject ShowTutorial;
    public bool isShow;
    public bool isHide;

    [Header("Skills")]
    public bool DASH;
    public bool SWAPDREAM;

    public GameObject ShowItemHud;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            CoreSingleton.Instance.gameManager.DASH = DASH;
            CoreSingleton.Instance.gameManager.SWAPDREAM = SWAPDREAM;
            StopCoroutine(nameof(IEDisable));
            isShow = true;
            ShowTutorial.SetActive(true);
            if(ShowItemHud != null)
            {
                ShowItemHud.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            isHide = true;
            StopCoroutine(nameof(IEDisable));
            StartCoroutine(nameof(IEDisable));
        }
    }

    IEnumerator IEDisable()
    {
        yield return new WaitForSeconds(1);
        ShowTutorial.SetActive(false);
    }

}
