using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementJump : MonoBehaviour, IListenUpSwipe
{

    private bool isJumping = false;
    private float jumpVelocity = 0;
    private float jumpDelta = 0;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float gravity = -9.8f;

    private void Update()
    {
        SimulateVerticalMovement();
    }

    public void SwipedUp()
    {
        Jump();
        
    }

    private void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            jumpVelocity = jumpForce;

        }
    }
    private void SimulateVerticalMovement()
    {
        if (isJumping)
        {
            jumpVelocity += gravity * Time.deltaTime;
            jumpDelta += jumpVelocity * Time.deltaTime;

            if (jumpDelta <= 0f)
            {
                isJumping = false;
                transform.localPosition += new Vector3(0, -jumpDelta, 0);
            }
            else
            {
                transform.localPosition += new Vector3(0, jumpVelocity * Time.deltaTime, 0);
            }
        }

    }
}
