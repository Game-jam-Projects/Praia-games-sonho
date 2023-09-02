using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace patrick
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputMap controls;
        private InputAction axis;

        private Rigidbody2D m_Rigidbody;
        private Animator animator;

        [SerializeField] private RuntimeAnimatorController dreamController;
        [SerializeField] private RuntimeAnimatorController nightmareController;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;

        private bool isLookLeft;
        private bool isGrounded;
        private bool isWalk;
        private bool preJump;
        [SerializeField] private LayerMask whatIsGround;

        private void Awake()
        {
            controls = new PlayerInputMap();
            axis = controls.Gameplay.Movement;

            controls.Gameplay.Jump.started += OnButtonSouth;
            controls.Gameplay.Jump.canceled += OnButtonSouth;

            controls.Gameplay.ButtonNorth.started += OnButtonNorth;
            controls.Gameplay.ButtonNorth.canceled += OnButtonNorth;

            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Gameplay.Jump.started -= OnButtonSouth;
            controls.Gameplay.Jump.canceled -= OnButtonSouth;

            controls.Gameplay.ButtonNorth.started -= OnButtonNorth;
            controls.Gameplay.ButtonNorth.canceled -= OnButtonNorth;

            Core.Instance.gameManager.ChagedStageType -= EChangeStageType;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            Core.Instance.gameManager.ChagedStageType += EChangeStageType;
        }


        // Update is called once per frame
        void Update()
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
            m_Rigidbody.velocity = new Vector2(axis.ReadValue<Vector2>().x * moveSpeed, m_Rigidbody.velocity.y);

            isWalk = axis.ReadValue<Vector2>().x != 0;

            if (axis.ReadValue<Vector2>().x > 0 && isLookLeft == true)
            {
                Flip();
            }
            else if (axis.ReadValue<Vector2>().x < 0 && isLookLeft == false)
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
            switch(stageType)
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
        private void OnButtonSouth(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Jump();
            }
        }

        private void OnButtonNorth(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Core.Instance.gameManager.ChangeStageType();
            }
        }

        #endregion
    }
}

