using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isDashingHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isDashingHash = Animator.StringToHash("isDashing");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        // Walking
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftwardPressed = Input.GetKey("a");
        bool rightwardPressed = Input.GetKey("d");

        if (!isWalking && forwardPressed || backwardPressed || leftwardPressed || rightwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if(isWalking && !forwardPressed && !backwardPressed && !leftwardPressed && !rightwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }


        // Dashing
        bool isDashing = animator.GetBool(isDashingHash);
        bool dashPressed = Input.GetKey("e");

        if (!isDashing && dashPressed)
        {
            animator.SetBool(isDashingHash, true);
        }
        if (isDashing && !dashPressed)
        {
            animator.SetBool(isDashingHash, false);
        }

        
        // Jumping
        bool isJumping = animator.GetBool(isJumpingHash);
        bool spaceButtonPressed = Input.GetButtonDown("Jump");

        if (!isJumping && spaceButtonPressed)
        {
            animator.SetBool(isJumpingHash, true);
            animator.SetBool(isWalkingHash, false);
        }
        if (isJumping && !spaceButtonPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }
        
        
    }
}
