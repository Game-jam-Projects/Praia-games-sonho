using System.Collections;
using DreamTeam.Runtime.Systems.Core;
using UnityEngine;

public class BackArea : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private StageType stageType;
    public float transitionDuration = 1.5f;

    public float pulseSpeed = 1.0f;     
    public float minScale = 3f;      
    public float maxScale = 3.2f;     
    private float time;

    private void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
    }

    private void Update()
    {
        PulsateObject();
    }


    void PulsateObject()
    {
        time += pulseSpeed * Time.deltaTime;
        float scaleValue = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(time, 1));
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    public void EChangeStageType(StageType stageType)
    {       
        this.stageType = stageType;
        StartCoroutine(nameof(FadeBetweenModes));
    }

    private IEnumerator FadeBetweenModes()
    {
        float timePassed = 0;

        switch (stageType)
        {
            case StageType.Nightmare:


                while (timePassed < transitionDuration)
                {
                    timePassed += Time.deltaTime;

                    float lerpValue = timePassed / transitionDuration;

                    Color color = spriteRenderer.color;
                    color.a = Mathf.Lerp(0, 0.98f, lerpValue);
                    spriteRenderer.color = color;

                    yield return null; // Aguarde o próximo frame
                }



                break;

            case StageType.Dream:

                while (timePassed < transitionDuration)
                {
                    timePassed += Time.deltaTime;

                    float lerpValue = timePassed / transitionDuration;

                    Color color = spriteRenderer.color;
                    color.a = Mathf.Lerp(0.98f, 0, lerpValue);
                    spriteRenderer.color = color;

                    yield return null; // Aguarde o próximo frame
                }


                break;
        }
    }
}
