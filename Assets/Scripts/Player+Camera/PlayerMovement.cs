using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int currentLane = 0;
    private float laneWidth = 2f;
    [SerializeField] private float speed = 5f;

    private bool isJumping = false;
    private float jumpVelocity = 0;
    private float jumpDelta = 0;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float gravity = -9.8f;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane < 1)
            {
                //move right
                currentLane += 1;
                
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane > -1)
            {
                //move left
                currentLane -= 1;
            }
        }
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        SimulateVerticalMovement();
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
    private void MovePlayer()
    {
        float targetXPos = currentLane * laneWidth;
        if (transform.localPosition.x == targetXPos) { return; }
        float moveDelta = targetXPos - transform.localPosition.x;
        float maxMoveDelta = speed * Time.deltaTime;
        if (transform.localPosition.x > targetXPos)
        {
            maxMoveDelta = -maxMoveDelta;
        }

        if (Mathf.Abs(moveDelta) <= Mathf.Abs(maxMoveDelta))
        {
            transform.localPosition += new Vector3(moveDelta, 0f, 0f);
        }
        else
        {
            transform.localPosition += new Vector3(maxMoveDelta, 0f, 0f);
        }
    }

}
