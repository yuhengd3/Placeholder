using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{
    public CharacterController controller;
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
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;
    private float originalStepOffset;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        originalStepOffset = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
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
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        /*
        // Jump
        // isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
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
        controller.Move(velocity * Time.deltaTime);
    }
}
