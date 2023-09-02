using DreamTeam.Runtime.Systems.Core;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    public Rigidbody2D _playerRB;
    private bool isLookLeft = false;
    public bool isGrounded;
    private Vector2 movementInput;
    private Animator animator;

    [Header("Ground")]
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundXSize, groundYSize;
    [Space]

    [Header("Wall")]
    public LayerMask whatIsWall;
    public Transform wallCheck;
    public float wallXSize, wallYSize;
    public bool isWall;
    public bool isJumpWall;
    public float jumpWallSideForce;
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

    [Header("Dream Animation")]
    [SerializeField] private RuntimeAnimatorController dreamController;
    [SerializeField] private RuntimeAnimatorController nightmareController;

    [Header("Dash System")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;



    private void Awake()
    {
        playerSr = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;

        inputReader.EnableInput();

        inputReader.OnButtonSouthDown += OnJump;
        inputReader.OnButtonNorthDown += OnButtonNorth;
        inputReader.OnButtonWestDown += OnButtonWest;
    }

    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;

        inputReader.OnButtonSouthDown -= OnJump;
        inputReader.OnButtonNorthDown -= OnButtonNorth;
        inputReader.OnButtonWestDown -= OnButtonWest;

        inputReader.DisableInput();
    }

    void Update()
    {
        movementInput = new Vector2(inputReader.Movement.x, inputReader.Movement.y);
        if (isJumpWall == false)
        {
            _playerRB.velocity = new Vector2(inputReader.Movement.x * velocity, _playerRB.velocity.y);
        }
        animator.SetFloat("speedY", _playerRB.velocity.y);
        animator.SetBool("isGrounded", isGrounded);

        if(isWall == true && _playerRB.velocity.y < 0)
        {
            _playerRB.gravityScale = 1;
        }
        else
        {
            _playerRB.gravityScale = 3.6f;
        }

        if(isDashing == true && isGrounded == true) { isDashing = false; }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(groundXSize, groundYSize), 0f, whatIsGround);
        isWall = Physics2D.OverlapBox(wallCheck.transform.position, new Vector2(wallXSize, wallYSize), 0f, whatIsWall);

        var isWalk = _playerRB.velocity != new Vector2(0, 0);

        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isGrounded", isGrounded);

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
    }


    private void OnJump()
    {
        if(isWall == true && isJumpWall == false)
        {
            isJumpWall = true;
            Vector2 dir = Vector2.zero;
            if(isLookLeft == true)
            {
                dir.Set(jumpWallSideForce, 0);
                
            }
            else
            {
                dir.Set(-jumpWallSideForce, 0);
            }

            Flip();

            _playerRB.velocity = dir;
            
            _playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Invoke("JumpWallOff", 0.3f);
        }

        if (isGrounded)
        {
            _playerRB.velocity = Vector2.zero;
            _playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            return;
        }

    }

    public void JumpWallOff()
    {
        isJumpWall = false;
    }
    private void OnDash()
    {
        

        if(isDashing == false && isGrounded == false)
        {
            StartCoroutine(Dash(inputReader.Movement.normalized));
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

    public void EChangeStageType(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Dream:
                animator.runtimeAnimatorController = dreamController;
                break;

            case StageType.Nightmare:
                animator.runtimeAnimatorController = nightmareController;
                break;
        }
    }

    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;

        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            GetComponent<Rigidbody2D>().velocity = direction * dashSpeed;
            yield return null;
        }

        //isDashing = false;
    }

    private void OnButtonNorth()
    {
        CoreSingleton.Instance.gameStateManager.ChangeStageType();
    }

    private void OnButtonWest()
    {
        OnDash();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector2(groundXSize, groundYSize));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallCheck.transform.position, new Vector2(wallXSize, wallYSize));
    }
}
