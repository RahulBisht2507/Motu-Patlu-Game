using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Animation")]
    public Animator motu;
    [SerializeField] private BossEmemy boss;
    [Header("Movement")]
    public float moveSpeed;

    [Header("Camera Rotation")]
    [SerializeField] private StarterAssetsInputs _input;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    [SerializeField]private PlayerInput player;
    private const float _threshold = 0.01f;
    public bool LockCameraPosition = false;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;


    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;


    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;
    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return player.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }


    [Header("Move")]
    [SerializeField]private GameObject _mainCamera;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Header("Player")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float punchCoolDown,lasttime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    bool readyToJump = true;
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private ParticleSystem jump;
    [SerializeField] private PickUpWeapon weapon;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject weapontrail;
    public bool moving,Axe,Sword;
    bool punch = true;
    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Ground Check")]
    public float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    public bool grounded;

    [Header("Stamina")]
    public bool stamina;
    [SerializeField] private StaminaSystem stam;
    [SerializeField] private HealthDemo health;
    [SerializeField] private ShopSystem shop;

    [Header("Player Audio Source")]
    public AudioSource walk;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource FastStep;

    [Header("Weapon Audio")]
    [SerializeField] private AudioSource AxeSound;
    [SerializeField] private AudioSource SwordSound;
    [SerializeField] private AudioSource punchSound;

   /* [Header("Samosa TimeCounter")]
    public Image speedSamosa;*/

   [SerializeField]private Transform orientation;
    float horizontalInput;
    float verticalInput;
/*    [SerializeField] private FixedJoystick joystick;*/
    Vector3 moveDirection;

    [Header("Custom Camera Input")]
    [SerializeField] Transform Player;
    [SerializeField] Transform playerObj;
    [SerializeField] private float rotationSpeed;

    [Header("bool and Rigidbody")]
    Rigidbody rb;
    float ySpeed;
    bool isGrounded;
    bool isJumping;
    bool moviing;
    bool jumpsounf = true;
    bool speedsamosa, ujkasamosa;
    int count;

    [Header("Mobile Inputs")]
    private Vector2 movement;
    private VariableJoystick joystic;
    private FixedTouchField touchField;
    private void Start()
    {
        player = GetComponent<PlayerInput>();
        joystic = FindAnyObjectByType<VariableJoystick>();
        touchField = FindAnyObjectByType<FixedTouchField>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        FastStep.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

   
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (touchField.Look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += touchField.Look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += touchField.Look.y * deltaTimeMultiplier;
        }
        // this code is not from unity tps starter Asset



        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    

    private void Update()
    {
        
       
        // Update Starts
        Audio();
        // ground check
        grounded = Physics.Raycast(orientation.position, Vector3.down,playerHeight*0.5f+-1.5f , whatIsGround);
        Debug.DrawRay(orientation.position, Vector3.down*.5f,Color.green);
        if (grounded)
        {
            Debug.DrawRay(orientation.position, Vector3.down * .5f, Color.red);
            /*Debug.Log("grounded");*/
            motu.SetBool("isFalling", false);
            motu.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(health.health<91)
            {
                if (shop.count > 0)
                {
                    health.health += 10;
                    health.bar.value = health.health;
                    shop.count--;
                }
            }
        }

        // Punch Animation
            if (Input.GetKeyDown(KeyCode.Q) || _input.Fire)
            {
                if (punch && weapon.MeeleMode)
                {
                    if (Time.time - lasttime < punchCoolDown)
                    {
                        return;
                    }
                    else
                    {
                        lasttime = Time.time;
                        punchSound.Play();
                        Debug.Log("true");
                        motu.ResetTrigger("notpunch");
                        motu.SetTrigger("punch");
                        Invoke("notp", .35f);
                        punch = false;
                    }
                }
                if (weapon.AxeMode)
                {
                Axe = true;
                    if (Axe)
                    {
                        AxeSound.Play();
                        motu.SetTrigger("AxeAttack");
                        motu.ResetTrigger("NotAxeAttack");
                        Invoke("notp", 1.3f);
                        Axe = false;
                        weapontrail.SetActive(true); 
                    }
                }
            if (weapon.SwordMode)
                {
                    Sword = true;
                    if (Sword)
                    {
                        SwordSound.Play();
                        motu.SetTrigger("SwordAttack");
                        motu.ResetTrigger("NotSwordAttack");
                        Invoke("notp", 1.3f);
                        Sword = false;
                }
                }
            }
        
        

        // Jumping Animation
        if (isJumping)
        {
            
            motu.SetBool("isJumping", false);
        }

        // Input and Speed Mechanics
        MyInput();
        SpeedControl();
        

        /*ySpeed = Physics.gravity.y * Time.deltaTime;
        if (!grounded)
        {
            motu.SetBool("isFalling", true);
        }*/


        // Jump Mechanics And Animations
        if ((Input.GetKeyDown(KeyCode.Space))&&grounded && readyToJump)
        {
            JumpMobile();
        }

        // handle drag
        if (grounded){
            isGrounded = true;
            isJumping = false;
            rb.drag = groundDrag;
            /*trail.enabled = false;*/
        }
        else {
            walk.enabled = false;
            isGrounded = false;
            rb.drag = 0;
            trail.enabled = true;
        }



        // Update Ends
    }

    public void JumpMobile()
    {
        if (grounded)
        {
            jump.transform.SetParent(null);
            jump.Play();
            Invoke("DestroyJump", 1.9f);
            isJumping = true;
            Debug.Log("Jump INitiatrded");
            Jump();
            if (ujkasamosa)
            {
                jumpForce = 20f;

            }
            readyToJump = false;
            motu.SetBool("isJumping", true);
            Invoke("ResetJump", jumpCooldown);
        }
    }
    public void DestroyJump()
    {
        jump.transform.position = smoke.transform.position;
        jump.transform.SetParent(this.transform);
    }
    public void notp()
    {
            motu.ResetTrigger("punch");
            motu.SetTrigger("notpunch");
            punch = true;
        if (Axe == false)
        {
            weapontrail.SetActive(false);
            motu.ResetTrigger("AxeAttack");
            Axe = true;
            motu.SetTrigger("NotAxeAttack");
        }
        if(Sword == false)
        {
            motu.ResetTrigger("SwordAttack");
            Sword = true; 
            motu.SetTrigger("NotSwordAttack");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.X) && shop.speedcount > 0)
        {
            speedsamosa = true;
            Invoke("ResetSpeed", 10f);
            shop.speedcount--;
        }
        if (Input.GetKeyDown(KeyCode.R) & shop.ujkacount > 0)
        {
            shop.ujkacount--;
            ujkasamosa = true;
            Invoke("ResetUjka", 10);
        }


        /*if(horizontalInput == 1 || horizontalInput == -1 || verticalInput == 1 || verticalInput == -1)
        {
            
        }*/
        if (grounded)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)|| _input.move !=Vector2.zero )
            {
                walk.enabled = true;
                    motu.SetBool("isMoving", true);
                if (stam.CanRun)
                {
                        if (Input.GetKey(KeyCode.LeftShift) && !speedsamosa)
                        {
                            Sprint();
                        }else if (speedsamosa)
                    {
                        moveSpeed = 35f;
                    }
                    else
                    {
                        stamina = false;
                        smoke.Stop();
                        motu.SetBool("isSprinting", false);
                        moveSpeed = 20f;
                        trail.enabled = false;
                        FastStep.enabled = false;
                    }
                }
                else
                {
                    
                    stamina = false;
                    smoke.Stop();
                    motu.SetBool("isSprinting", false);
                    moveSpeed = 20f;
                    trail.enabled = false;
                    FastStep.enabled = false;
                }
            }
            else
            {
                FastStep.enabled = false;
                walk.enabled = false;
                motu.SetBool("isSprinting", false);
                motu.SetBool("isMoving", false);
            }
        }else
        {
            
            if (boss.fall)
            {

            }
            else
            {
                motu.SetBool("isMoving", false);
                motu.SetBool("isFalling", true);
            }
        }

        // when to jump
    }

    public void Sprint()
    {
        stamina = true;
        walk.enabled = false;
        smoke.Play();
        FastStep.enabled = true;
        motu.SetBool("isSprinting", true);
        moveSpeed = 25f;
        trail.enabled = true;
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * _input.move.y+ orientation.right *_input.move.x;
       

            // on ground
            if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // in air
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // normalise input direction
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 1f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }
    private void ResetJump()
    {
        readyToJump = true;
        if (boss.fall == true)
        {

        }
        else
        {
            motu.SetBool("isFalling", true);
            motu.SetBool("isJumping", false);
        }
    }

    private void Audio()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsounf)
            {
                jumpSound.Play();
                Invoke("ResetJumpSound", 1.5f);
                jumpsounf = false;
            }
        }
        else
        {
         
        }
    }
    private void ResetJumpSound()
    {
        jumpsounf = true;
    }
    private void ResetSpeed()
    {
        speedsamosa = false;
    }
    private void ResetUjka()
    {
        ujkasamosa = false;
        jumpForce = 10f;
    }


    public void Attack()
    {
        if (punch && weapon.MeeleMode)
        {
            if (Time.time - lasttime < punchCoolDown)
            {
                return;
            }
            else
            {
                lasttime = Time.time;
                punchSound.Play();
                Debug.Log("true");
                motu.ResetTrigger("notpunch");
                motu.SetTrigger("punch");
                Invoke("notp", .35f);
                punch = false;
            }
        }
        if (weapon.AxeMode)
        {
            Axe = true;
            if (Axe)
            {
                AxeSound.Play();
                motu.SetTrigger("AxeAttack");
                motu.ResetTrigger("NotAxeAttack");
                Invoke("notp", 1.3f);
                Axe = false;
                weapontrail.SetActive(true);
            }
        }
        if (weapon.SwordMode)
        {
            Sword = true;
            if (Sword)
            {
                SwordSound.Play();
                motu.SetTrigger("SwordAttack");
                motu.ResetTrigger("NotSwordAttack");
                Invoke("notp", 1.3f);
                Sword = false;
            }
        }
    }
}
