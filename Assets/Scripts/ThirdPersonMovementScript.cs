using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    public Transform cam;

    // Walk & Sprint
    //private Rigidbody rb;
    public float baseSpeed = 6f;
    private float currentSpeed;
    public float sprint = 6f;

    // smooth the player's turning
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Dash & Movement
    public Vector3 moveDir;

    // Jump
    Vector3 velocity;
    public float toGroundDistance;
    public float jumpSpeed = 3f;
    public float jumpButtonGracePeriod = 3f;
    public float gravity = -9.8f;
    //public Transform groundCheck;
    //public float groundDist;
    //public LayerMask groundMask;
    //bool isGrounded;
    //public float jumpHeight = 3;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool jumping;
    private bool grounded;
    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        originalStepOffset = controller.stepOffset;
    }

    Vector3 initialPosition = new Vector3();

    IEnumerator JumpTimer()
    {
        /*
        while(controller.isGrounded)
        {
            yield return new WaitForSeconds(1);
            yield return new WaitForEndOfFrame();
        }*/
        initialPosition = transform.position;
        yield return new WaitForSeconds(jumpButtonGracePeriod);
        yield return new WaitWhile(()=>!grounded);
        animator.SetBool("isJumping", false);
    }

    void IsGrounded()
    {
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, -transform.up, out hit, toGroundDistance, 1 << LayerMask.NameToLayer("Floor"));
        Debug.DrawLine(transform.position, transform.position - Vector3.down * toGroundDistance, Color.red, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, -transform.up, out hit, toGroundDistance);
        Debug.Log("isGrounded" + grounded);

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = baseSpeed + sprint;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        // WASD direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            // make the player turn to the direction of the camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // WASD moving
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (!jumping)
            {
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
            }
            else
            {
                // position = initial position + initial velocity * times - 0.5 * acceleration * time^2
                float y = initialPosition.y + jumpSpeed * Time.deltaTime - 0.5f * gravity * Time.deltaTime * Time.deltaTime;
                Vector3 move = new Vector3(moveDir.normalized.x * currentSpeed, y, moveDir.normalized.z * currentSpeed);
                Debug.Log(move);
                controller.Move(move * Time.deltaTime);
            }
        }

        //velocity.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(JumpTimer());
            animator.SetBool("isJumping", true);
        }

        /*
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        //if (controller.isGrounded)
        {

            controller.stepOffset = originalStepOffset;
            velocity.y = -0.5f;
            animator.SetBool("isGrounded", true);
            grounded = true;
            animator.SetBool("isJumping", false);
            jumping = false;
            animator.SetBool("isFalling", false);

            if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            //if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpSpeed;
                animator.SetBool("isJumping", true);
                jumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            controller.stepOffset = 0;
            animator.SetBool("isGrounded", false);
            grounded = false;

            if((jumping && velocity.y < 0) || velocity.y < -100)
            {
                animator.SetBool("isFalling", true);
            }
        }
        */
        
        /*
        // Jump
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (controller.isGrounded)
        {
            controller.stepOffset = originalStepOffset;
            velocity.y = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                
            }
        }
        else
        {
            controller.stepOffset = 0;
        }
        //rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        */
        //controller.Move(velocity * Time.deltaTime);
    }
}
