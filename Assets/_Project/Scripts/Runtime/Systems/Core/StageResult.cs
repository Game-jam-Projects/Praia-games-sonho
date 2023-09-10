using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.System.Core;

public class StageResult : MonoBehaviour
{

    public TMP_Text sonhos;
    public TMP_Text mortes;
    public TMP_Text tempo;
    // Start is called before the first frame update
    void Start()
    {
        sonhos.text = "Sonhos coletados, " + CoreSingleton.Instance.collectiblesManager.collected.Count.ToString() + " de "+ CoreSingleton.Instance.collectiblesManager.totalCollectableDream.ToString();
        mortes.text = "Quantidade de mortes: " + CoreSingleton.Instance.gameManager.GetDeathCount().ToString();
        tempo.text = "Tempo Jogado: " + ConvertToHoursMinutesSeconds(Mathf.RoundToInt(CoreSingleton.Instance.gameManager.chrono.GetElapsedTime()));

        StartCoroutine(nameof(IEGoCut));
    }

    public static string ConvertToHoursMinutesSeconds(int totalSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(nameof(IEGoCut));
            SceneLoader.Instance.NextScene();
        }
    }

    IEnumerator IEGoCut()
    {
        yield return new WaitForSeconds(10);
        SceneLoader.Instance.NextScene();
    }
}
