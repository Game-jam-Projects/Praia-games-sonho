using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public Rigidbody2D _playerRB;
    private bool isLookLeft = false;
    public bool isGrounded;
    private Vector2 movementInput;
    private Animator _playerAin;

    [Header("Ground")]
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundXSize, groundYSize;
    [Space]

    [Header("Variables")]
    public float velocity = 2f;
    public float jumpForce = 100f;
    private float speedY;
    [Space]

    [Header("Sprite renderer")]
    public SpriteRenderer playerSr;
    public Color playerNoColor;
    public Color defaultColor;

    [Header("SFX")]
    public ParticleSystem landingParticles;
    private bool _wasGrounded = true;
    [SerializeField] Transform positionParticles;

    private void Awake()
    {
        playerSr = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAin = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        inputReader.OnJumpDown += OnJump;
    }


    private void OnDisable()
    {
        inputReader.OnJumpDown -= OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(inputReader.movement.x, inputReader.movement.y);
        _playerRB.velocity = new Vector2(inputReader.movement.x * velocity, _playerRB.velocity.y);

        _playerAin.SetFloat("speedY", _playerRB.velocity.y);
        _playerAin.SetBool("isGround", isGrounded);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(groundXSize, groundYSize), 0f, whatIsGround);

        if (_playerRB.velocity != new Vector2(0, 0))
        {
            _playerAin.SetBool("isWalk", true);
        }
        else
        {
            _playerAin.SetBool("isWalk", false);
        }

        //virar o player
        if (movementInput.x < 0 && isLookLeft == false)
        {
            Flip();
        }
        else if (movementInput.x > 0 && isLookLeft == true)
        {

            Flip();
        }
        if (isGrounded && !_wasGrounded)
        {
            PlayLandingParticles();

        }
        _wasGrounded = isGrounded;


        _playerAin.SetBool("isGround", isGrounded);

    }


    private void OnJump()
    {

        if (isGrounded)
        {
            _playerRB.velocity = Vector2.zero;
            _playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            return;
        }

    }

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1; //Inverte o sinal do scale X
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    private void PlayLandingParticles()
    {
        if (landingParticles != null)
        {
            landingParticles.transform.position = positionParticles.position;
            landingParticles.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector2(groundXSize, groundYSize));
    }
}
