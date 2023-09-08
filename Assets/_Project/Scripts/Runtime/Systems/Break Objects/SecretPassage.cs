using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamTeam.Runtime.Systems.Core;

public class SecretPassage : MonoBehaviour, IBreakObjects
{
    public List<StageType> breakableIn;
    public Animator[] breakables;
    public Collider2D collider;
    private DreamNightmareObjectManager dreamManager;
    public SpriteRenderer[] HiddenWall;
    public float transitionDuration;
    public GameObject GrapArea;
    void Start()
    {
        dreamManager = GetComponentInChildren<DreamNightmareObjectManager>();
    }

    public void BreakObject()
    {
        if(breakableIn.Contains(CoreSingleton.Instance.gameStateManager.GetStageType()))
        {
            dreamManager.dontChange = true;
            if(GrapArea != null)
            {
                GrapArea.SetActive(false);
            }
            print("Som de Parede sendo Quebrada");
            foreach(Animator anim in breakables)
            {
                anim.SetTrigger("break");
            }
            collider.enabled = false;
            StartCoroutine(nameof(FadeWall));
        }
    }

    private IEnumerator FadeWall()
    {
        float timePassed = 0;

        while (timePassed < transitionDuration)
        {
            timePassed += Time.deltaTime;

            float lerpValue = timePassed / transitionDuration;

            // Fazendo fade nos renderers do sonho
            foreach (SpriteRenderer renderer in HiddenWall)
            {
                Color color = renderer.color;
                color.a = Mathf.Lerp(1, 0, lerpValue);
                renderer.color = color;
            }

            yield return null; // Aguarde o próximo frame
        }

        foreach (SpriteRenderer renderer in HiddenWall)
        {
            renderer.gameObject.SetActive(false);
        }
    }
}
