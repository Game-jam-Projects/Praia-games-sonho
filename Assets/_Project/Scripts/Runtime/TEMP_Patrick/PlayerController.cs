using DreamTeam.Runtime.Systems.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace patrick
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;

        private Rigidbody2D m_Rigidbody;
        private Animator animator;

        [SerializeField] private RuntimeAnimatorController dreamController;
        [SerializeField] private RuntimeAnimatorController nightmareController;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;

        private Vector2 axis;

        private bool isLookLeft;
        private bool isGrounded;
        private bool isWalk;
        private bool preJump;
        [SerializeField] private LayerMask whatIsGround;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {   
            axis = inputReader.Movement;

            inputReader.OnButtonNorthDown += OnButtonSouth;
            inputReader.OnButtonSouthUp += OnButtonSouth;

            inputReader.OnButtonNorthDown += OnButtonNorth;

            CoreSingleton.Instance.gameStateManager.ChagedStageType += EChangeStageType;
        }

        private void OnDisable()
        {
            inputReader.OnButtonNorthDown -= OnButtonSouth;
            inputReader.OnButtonSouthUp -= OnButtonSouth;

            inputReader.OnButtonNorthDown -= OnButtonNorth;

            CoreSingleton.Instance.gameStateManager.ChagedStageType -= EChangeStageType;
        }

        private void Update()
        {
            MovePlayer();
            animator.SetFloat("speedY", m_Rigidbody.velocity.y);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("isWalk", isWalk);
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.2f, whatIsGround);
        }

        private void MovePlayer()
        {
            m_Rigidbody.velocity = new Vector2(axis.x * moveSpeed, m_Rigidbody.velocity.y);

            isWalk = axis.x != 0;

            if (axis.x > 0 && isLookLeft == true)
            {
                Flip();
            }
            else if (axis.x < 0 && isLookLeft == false)
            {
                Flip();
            }
        }

        private void Jump()
        {
            if (isGrounded == true && preJump == false)
            {
                preJump = true;
                animator.SetTrigger("Jump");
            }
        }

        public void AEJump()
        {
            preJump = false;
            m_Rigidbody.velocity = Vector2.zero;
            m_Rigidbody.AddForce(Vector2.up * jumpForce);
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

        void Flip()
        {
            isLookLeft = !isLookLeft;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        #region INPUT
        private void OnButtonSouth()
        {
            Jump();
        }

        private void OnButtonNorth()
        {
            CoreSingleton.Instance.gameStateManager.ChangeStageType();
        }

        #endregion
    }
}

