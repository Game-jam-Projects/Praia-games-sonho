using DreamTeam.Runtime.Systems.Core;
using DreamTeam.Runtime.Systems.Health;
using PainfulSmile.Runtime.Utilities.AutoTimer.Core;
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
    private bool isWallSlide;
    public bool isJumpWall;
    public float jumpWallSideForce;

    [Header("Gripping System")]
    [SerializeField] private float gripTime = 3f;
    [SerializeField] private float gripTimer;
    [SerializeField] private float grippingSpeed;
    private bool isGripping = false;
    private bool canGrip = true;
    private bool isClimbable;
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
    
    [Header("Fly System")]
    public float flySpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public float maxFlyTime = 0f;
    
    private bool isFlying = false;

    [SerializeField] private AutoTimer changeStageCooldown;
    private bool canChangeStage = true;

    private HealthSystem healthSystem;
    private bool isTriggeredDeadAnimation;


    #region UNITY

    private void Awake()
    {
        playerSr = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
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

        healthSystem.OnRevive += Revive;

        SelectedPlayer();
        //GameManager.Instance.chrono.Start();
    }
    
    private void OnDestroy()
    {
        CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;

        inputReader.OnButtonSouthDown -= OnJump;
        inputReader.OnButtonNorthDown -= OnButtonNorth;
        inputReader.OnButtonWestDown -= OnButtonWest;
        inputReader.OnRightTriggerDown -= OnRightTriggerDown;
        inputReader.OnRightTriggerUp -= OnRightTriggerUp;

        healthSystem.OnRevive -= Revive;

        inputReader.DisableInput();
    }

    void Update()
    {
        if (healthSystem.IsDie)
        {
            if (isTriggeredDeadAnimation == false)
            {
                isTriggeredDeadAnimation = true;
                animator.SetBool("IsDead", true);
            }

            animator.SetFloat("speedY", 0f);
            animator.SetBool("isWalk", false);
            animator.SetBool("isWall", false);
            animator.SetBool("isWalkWall", false);
            animator.SetBool("isWallSlide", false);
            animator.SetBool("isGripWall", false);
            animator.SetBool("isDash", false);
            animator.SetBool("Fly", false);

            maxFlyTime = 0;

            _playerRB.velocity = Vector2.zero;
            return;
        }

        movementInput = new Vector2(inputReader.Movement.x, inputReader.Movement.y);

        if (isFlying == true)
        {
            TemporaryFly();
            return;
        }

        Vector2 plaveVelocity = Vector2.zero;
        if (isGrounded == true)
        {
            plaveVelocity = new Vector2(inputReader.Movement.x * velocity, _playerRB.velocity.y);
        }
        else if (isGrounded == false && isJumpWall == true)
        {
            plaveVelocity = _playerRB.velocity;
        }
        else if (isGrounded == false)
        {
            if (movementInput.x != 0)
            {
                plaveVelocity = new Vector2(inputReader.Movement.x * velocity, _playerRB.velocity.y);
            }
            else
            {
                plaveVelocity = _playerRB.velocity;
            }
        }

        _playerRB.velocity = plaveVelocity;

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

        if (isShowEcho == true)
        {
            Echo();
        }

        GravityManager();

        bool isWalk = movementInput.x != 0;
        bool isWalkWall = movementInput.y != 0;
        animator.SetFloat("speedY", _playerRB.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isWalkWall", isWalkWall);
        animator.SetBool("isWall", isWall);
        animator.SetBool("isWallSlide", isWallSlide);
        animator.SetBool("isGripWall", isGripping);
        animator.SetBool("isDash", isShowEcho);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        if (healthSystem.IsDie) return;

        isGrounded = Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(groundXSize, groundYSize), 0f, whatIsGround);
        Collider2D wall = Physics2D.OverlapBox(wallCheck.transform.position, new Vector2(wallXSize, wallYSize), 0f, whatIsWall);
        isWall = wall != null;
        if (wall != null)
        {
            if (wall.TryGetComponent<WallSystem>(out WallSystem wallSystem))
            {
                isClimbable = wallSystem.isClimbable;
            }
            else
            {
                isClimbable = false;
            }
        }
        else
        {
            isClimbable = false;
        }

        isWallSlide = isWall && _playerRB.velocity.y < 0;
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
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Foley/Fall", transform.position);
            PlayLandingParticles();
        }

        _wasGrounded = isGrounded;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SecretPassage"))
        {
            if (isShowEcho == true)
            {
                collision.gameObject.GetComponent<IBreakObjects>().BreakObject();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collision.GetComponent<ICollectible>().Collect();

        }
        else if (collision.gameObject.CompareTag("WallExit"))
        {
            if (isGripping == false) { return; }
            isGripping = false;
            _playerRB.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            animator.SetBool("isGripWall", isGripping);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheck.transform.position, new Vector2(groundXSize, groundYSize));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(wallCheck.transform.position, new Vector2(wallXSize, wallYSize));
    }

    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        isShowEcho = true;
        float startTime = Time.time;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Foley/Dash", transform.position);
        while (Time.time < startTime + dashDuration)
        {
            _playerRB.velocity = direction * dashSpeed;
            yield return null;
        }

        _playerRB.velocity = Vector2.zero;
        isShowEcho = false;
        //isDashing = false;
    }

    #endregion

    public void SelectedPlayer()
    {
        Character selectedChar = CoreSingleton.Instance.gameManager.GetCharacter();
        dreamController = selectedChar.dreamController;
        nightmareController = selectedChar.nightmareController;
        animator.runtimeAnimatorController = dreamController;
    }

    private void Revive()
    {
        isTriggeredDeadAnimation = false;
        animator.SetBool("IsDead", false);
        transform.eulerAngles = Vector2.zero;
        isFlying = false;
    }
    
    private void GripWall()
    {
        if (healthSystem.IsDie) return;

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
        if (healthSystem.IsDie) return;

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
    
    private void Dash()
    {
        if(CoreSingleton.Instance.gameManager.DASH == false) { return; }
        if(isGripping == true) { return; }
        if(movementInput.sqrMagnitude == 0) { return; }

        if (isDashing == false && isShowEcho == false && isFlying == false)
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

    public bool GetLookLeft()
    {
        return isLookLeft;
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
        if (healthSystem.IsDie) return;

        if (landingParticles != null)
        {
            landingParticles.transform.position = positionParticles.position;
            landingParticles.Play();
        }
    }

    private void TemporaryFly()
    {
        if (maxFlyTime <= 0)
        {
            isFlying = false;
            transform.eulerAngles = Vector3.zero;
            animator.SetBool("Fly", false);
            return;
        }

        float rotationAmount = movementInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotationAmount);

        Vector2 direction = transform.up;
        _playerRB.velocity = direction * flySpeed;

        maxFlyTime -= Time.deltaTime;
    }

    private void GravityManager()
    {
        if (healthSystem.IsDie) return;

        if (isGripping)
        {
            _playerRB.gravityScale = 0;
        }
        else if(isWall == true && _playerRB.velocity.y < 0)
        {
            _playerRB.gravityScale = 1;
        }
        else
        {
            _playerRB.gravityScale = 4f;
        }
    }





    #region COLLECT ITEM

    public void DashCrystal()
    {
        if (healthSystem.IsDie) return;
        gripTimer = 0;
        isDashing = false;
    }

    public void Fly(Vector3 startEuler, float flightTime)
    {
        if (healthSystem.IsDie) return;

        maxFlyTime += flightTime;

        if (isFlying == false)
        {
            transform.eulerAngles = startEuler;
        }
        isFlying = true;
        animator.SetBool("Fly", true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Foley/Fly", transform.position);

    }

    #endregion

    #region EVENT CALLS
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

    #endregion

    #region INPUT

    private void OnButtonNorth()
    {
        if (healthSystem.IsDie) return;

        if (CoreSingleton.Instance.gameManager.SWAPDREAM == false) { return; }

        if (CoreSingleton.Instance.gameManager.GetItem() <= 0) { return; }

        if (canChangeStage == false)
            return;

        CoreSingleton.Instance.gameManager.SetItem(-1);

        canChangeStage = false;
        CoreSingleton.Instance.gameStateManager.ChangeStageType();

        changeStageCooldown.Start(changeStageCooldown.InitTime);
        changeStageCooldown.OnExpire += () =>
        {
            canChangeStage = true;
        };
    }

    private void OnButtonWest()
    {
        if (healthSystem.IsDie) return;

        Dash();
    }

    private void OnRightTriggerDown()
    {
        if (healthSystem.IsDie) return;

        if (isClimbable == false) { return; }

        if (canGrip == true)
        {
            OnGrip(true);
        }
    }

    private void OnRightTriggerUp()
    {
        if (healthSystem.IsDie) return;

        OnGrip(false);
        _playerRB.gravityScale = 3.6f;
    }

    #endregion

    public InputReader GetInput()
    {
        return inputReader;
    }
}