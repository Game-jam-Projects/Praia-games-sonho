using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHiding : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [Range(0.5f,10)]
    [SerializeField] private float timeToDisable;
    [Range(0.5f,10)]
    [SerializeField] private float timeToEnable;

    [Range(0,2)]
    [SerializeField] private float timeTransition;
    [SerializeField] private bool isDisable;
    [SerializeField] private bool isPlatformDestroy;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDisable == false && collision.gameObject.CompareTag("Player"))
        {
            isDisable = true;
            StartCoroutine(nameof(IEDellayDisable));
        }
    }

    IEnumerator IEDellayDisable()
    {
        
        yield return new WaitForSeconds(timeToDisable);
        anim.SetTrigger("Disable");
        yield return new WaitForSeconds(1);
        FadeDisable();
    }

    IEnumerator IEDellayEnable()
    {
        yield return new WaitForSeconds(timeToEnable);
        EnableObject();
    }

    private void FadeDisable()
    {
        
        spriteRenderer.DOFade(0f, timeTransition).OnComplete(() =>
        {
            //platform.SetActive(false);
            if(isPlatformDestroy == false)
                StartCoroutine(nameof(IEDellayEnable));
        });
    }

    private void FadeEnable()
    {
        //platform.SetActive(true);
        spriteRenderer.DOFade(1f, timeTransition);
        anim.SetTrigger("Enable");
    }

    /// <summary>
    /// Função para chamar no evento de animação caso useno lugar do IEDellayDisable()
    /// </summary>
    public void DisableObject()
    {
        isDisable = true;
        FadeDisable();
    }

    public void EnableObject()
    {
        isDisable = false;
        FadeEnable();
    }

}
