using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int currentLane = 0;
    private float laneWidth = 2f;
    [SerializeField] private float speed = 5f;

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
