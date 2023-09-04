using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformOnOff : MonoBehaviour
{

    public GameObject A;
    public GameObject B;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(OnOFFSystem));
    }

    IEnumerator OnOFFSystem()
    {
        while(true)
        {
            yield return new WaitForSeconds(timer);
            A.SetActive(!A.activeSelf);
            B.SetActive(!B.activeSelf);
        }
    }
}
