using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformOnOff : MonoBehaviour
{

    public GameObject StartOn;
    public GameObject StartOff;

    public float timer;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartOn.SetActive(true);
        StartOff.SetActive(false);
        StartCoroutine(nameof(OnOFFSystem));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(OnOFFSystem));
    }

    IEnumerator OnOFFSystem()
    {
        while(true)
        {
            yield return new WaitForSeconds(timer);
            StartOn.SetActive(!StartOn.activeSelf);
            StartOff.SetActive(!StartOff.activeSelf);
        }
    }
}
