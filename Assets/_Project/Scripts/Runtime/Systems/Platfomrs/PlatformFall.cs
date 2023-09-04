using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamTeam.Runtime.Systems.Core;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformFall : MonoBehaviour
{
    [Range(0.1F, 7)]
    [SerializeField] private float delayToFall;
    [SerializeField] private Animator anim;
    private Rigidbody2D rbPlatform;
    private bool isFalling;
    private Vector3 startPosition;
    [SerializeField] private float fallTime;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rbPlatform = GetComponent<Rigidbody2D>();
        CoreSingleton.Instance.gameManager.OnTransitionFinished += ResetPlataform;
    }


    private void OnDisable()
    {
        CoreSingleton.Instance.gameManager.OnTransitionFinished -= ResetPlataform;
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
        StartCoroutine(nameof(IEFallController));

    }

    private IEnumerator IEFallController()
    {
        yield return new WaitForSeconds(fallTime);
        rbPlatform.velocity = Vector2.zero;
        rbPlatform.bodyType = RigidbodyType2D.Kinematic;
    }

    private void ResetPlataform()
    {
        isFalling = false;
        rbPlatform.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPosition;
    }
}
