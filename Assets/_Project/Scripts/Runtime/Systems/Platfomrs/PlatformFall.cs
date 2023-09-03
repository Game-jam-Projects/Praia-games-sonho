using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformFall : MonoBehaviour
{
    [Range(0.1F, 7)]
    [SerializeField] private float delayToFall;
    [SerializeField] private Animator anim;
    private Rigidbody2D rbPlatform;
    private bool isFalling;


    // Start is called before the first frame update
    void Start()
    {
        rbPlatform = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling == false && collision.gameObject.CompareTag("Player"))
        {
            Fall();
        }
    }

    private void Fall()
    {
        isFalling = true;
        anim.SetTrigger("ToFall");
        StartCoroutine(nameof(IEDellayToFall));
        
    }

    private IEnumerator IEDellayToFall()
    {
        yield return new WaitForSeconds(delayToFall);
        anim.enabled = false;
        anim.SetTrigger("ItFell");
        rbPlatform.bodyType = RigidbodyType2D.Dynamic;

    }
}
