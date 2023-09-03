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

    [Header("Gripping System")]
    [SerializeField] private float gripTime = 3f;
    [SerializeField] private float gripTimer;
    [SerializeField] private float grippingSpeed;
    private bool isGripping = false;
    private bool canGrip = true;
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
    private bool isShowEcho;
    public GameObject echoPrefab;
    public float timeBtwEcho;
    public float timecho;

    private void Awake()
    {
        playerSr = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        CoreSingleton.Instance.playerController = this;

        inputReader.EnableInput();

        inputReader.OnButtonSouthDown += OnJump;
        inputReader.OnButtonNorthDown += OnButtonNorth;
        inputReader.OnButtonWestDown += OnButtonWest;
        inputReader.OnRightTriggerDown += OnRightTriggerDown;
        inputReader.OnRightTriggerUp += OnRightTriggerUp;
    }
    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;

        inputReader.OnButtonSouthDown -= OnJump;
        inputReader.OnButtonNorthDown -= OnButtonNorth;
        inputReader.OnButtonWestDown -= OnButtonWest;
        inputReader.OnRightTriggerDown -= OnRightTriggerDown;
        inputReader.OnRightTriggerUp -= OnRightTriggerUp;

        inputReader.DisableInput();
    }
    void Update()
    {
        movementInput = new Vector2(inputReader.Movement.x, inputReader.Movement.y);
        if (isJumpWall == false)
        {
            _playerRB.velocity = new Vector2(inputReader.Movement.x * velocity, _playerRB.velocity.y);
            
        }
       

        if (isGripping == true && isWall == true)
        {
            GripWall();
        }
        else
        {
            isGripping = false;
        }

        if (isDashing == true && isGrounded == true) { isDashing = false; }
        if (canGrip == false && isGrounded == true)
        {
            canGrip = true;
            gripTimer = 0;
        }

        if(isShowEcho == true)
        {
            Echo();
        }

        GravityManager();

        animator.SetFloat("speedY", _playerRB.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
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
    private void GripWall()
    {
        if (gripTimer < gripTime)
        {            
            if(movementInput.y != 0)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, movementInput.y * grippingSpeed);
            }
            else
            {
            _playerRB.velocity = Vector2.zero;
            }
            isGripping = true;
            gripTimer += Time.deltaTime;
        }
        else
        {
            isGripping = false;
            canGrip = false;            
        }
    }

    private void OnJump()
    {
        if (isWall == true && isJumpWall == false)
        {
            isJumpWall = true;
            isGripping = false;
            Vector2 dir = Vector2.zero;
            if (isLookLeft == true)
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
        if (isDashing == false && isShowEcho == false)
        {
            StartCoroutine(Dash(inputReader.Movement.normalized));
        }
    }
    private void OnGrip(bool isGrip)
    {
        if (isWall == false) { return; }

        isGripping = isGrip;
    }
    public void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1; //Inverte o sinal do scale X
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    public void Echo()
    {
        if(timecho >= timeBtwEcho)
        {
            GameObject echoInstance = Instantiate(echoPrefab, transform.position, Quaternion.identity);
            if(isLookLeft == true)
            {
                echoInstance.GetComponent<SpriteRenderer>().flipX = true;
            }
            Destroy(echoInstance, 0.25f);
            timecho = 0;
        }
        else
        {
            timecho += Time.deltaTime;
        }
    }
    private void PlayLandingParticles()
    {
        if (landingParticles != null)
        {
            landingParticles.transform.position = positionParticles.position;
            landingParticles.Play();
        }
    }

    private void GravityManager()
    {
        if(isGripping)
        {
            _playerRB.gravityScale = 0;
        }
        else if(isWall == true && _playerRB.velocity.y < 0)
        {
            _playerRB.gravityScale = 1;
        }
        else
        {
            _playerRB.gravityScale = 3.6f;
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
        isShowEcho = true;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            _playerRB.velocity = direction * dashSpeed;
            yield return null;
        }

        _playerRB.velocity = Vector2.zero;
        isShowEcho = false;
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

    private void OnRightTriggerDown()
    {
        if (canGrip == true)
        {
            OnGrip(true);
        }
    }

    private void OnRightTriggerUp()
    {
        OnGrip(false);
        _playerRB.gravityScale = 3.6f;
    }

    public void NewDash()
    {
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("SecretPassage"))
        {
            if (isShowEcho == true)
            {
                collision.gameObject.GetComponent<IBreakObjects>().BreakObject();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            collision.GetComponent<ICollectible>().Collect();
            _playerRB.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector2(groundXSize, groundYSize));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallCheck.transform.position, new Vector2(wallXSize, wallYSize));
    }
}