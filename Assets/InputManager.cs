using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    Vector2 touchDown;
    Vector2 touchUp;

    private void Update()
    {
        CheckTouches();
        CheckKeys();
    }
    private void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //move right
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //move left
        }
    }
    private void CheckTouches()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                touchDown = new Vector2(touch.position.x, touch.position.y);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchUp = new Vector2(touch.position.x, touch.position.y);
                CalculateTouchDirection();
            }
        }
    }

    private void CalculateTouchDirection()
    {

        Vector2 touchDelta = touchUp - touchDown;
        touchDelta.Normalize();
        if (touchDelta.y > 0 && touchDelta.x > -0.5f && touchDelta.x < 0.5f)
        {
            
        }
        else if (touchDelta.y < 0 && touchDelta.x > -0.5f && touchDelta.x < 0.5f)
        {
            
        }
        else if (touchDelta.x > 0 && touchDelta.y > -0.5f && touchDelta.y < 0.5f)
        {
            
        }
        else if (touchDelta.x < 0 && touchDelta.y > -0.5f && touchDelta.y < 0.5f)
        {
            
        }
    }
}
