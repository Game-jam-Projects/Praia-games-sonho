using DreamTeam.Runtime.Systems.Core;
using UnityEngine;
using System.Collections;

public class DreamNightmareObjectManager : MonoBehaviour
{
    public float transitionDuration;
    public GameObject dream;
    public GameObject nigthmare;

    
    [SerializeField] private SpriteRenderer[] dreamRenderes;
    [SerializeField] private SpriteRenderer[] nightmareRenderes;

    private StageType stageType;

    public bool dontChange;

    // Start is called before the first frame update
    void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        LoadRenderes();
    }

    private void LoadRenderes()
    {
        dreamRenderes = dream.GetComponentsInChildren<SpriteRenderer>();
        nightmareRenderes = nigthmare.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnDisable()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;
    }

    public void EChangeStageType(StageType stageType)
    {
        if(dontChange == true) { return; }
       this.stageType = stageType;       
       StartCoroutine(nameof(FadeBetweenModes));
    }

    private IEnumerator FadeBetweenModes()
    {
        float timePassed = 0;

        switch(stageType)
        {
            case StageType.Nightmare:

                nigthmare.SetActive(true);

                while (timePassed < transitionDuration)
                {
                    timePassed += Time.deltaTime;

                    float lerpValue = timePassed / transitionDuration;

                    // Fazendo fade nos renderers do sonho
                    foreach (SpriteRenderer renderer in dreamRenderes)
                    {
                        Color color = renderer.color;
                        color.a = Mathf.Lerp(1, 0, lerpValue);
                        renderer.color = color;
                    }

                    // Fazendo fade nos renderers do pesadelo
                    foreach (SpriteRenderer renderer in nightmareRenderes)
                    {
                        Color color = renderer.color;
                        color.a = Mathf.Lerp(0, 1, lerpValue);
                        renderer.color = color;
                    }

                    yield return null; // Aguarde o próximo frame
                }

                dream.SetActive(false);

                break;

            case StageType.Dream:


                dream.SetActive(true);

                while (timePassed < transitionDuration)
                {
                    timePassed += Time.deltaTime;

                    float lerpValue = timePassed / transitionDuration;

                    // Fazendo fade nos renderers do sonho
                    foreach (SpriteRenderer renderer in dreamRenderes)
                    {
                        Color color = renderer.color;
                        color.a = Mathf.Lerp(0, 1, lerpValue);
                        renderer.color = color;
                    }

                    // Fazendo fade nos renderers do pesadelo
                    foreach (SpriteRenderer renderer in nightmareRenderes)
                    {
                        Color color = renderer.color;
                        color.a = Mathf.Lerp(1, 0, lerpValue);
                        renderer.color = color;
                    }

                    yield return null; // Aguarde o próximo frame
                }

                nigthmare.SetActive(false);


                break;
        }      
    }


}
