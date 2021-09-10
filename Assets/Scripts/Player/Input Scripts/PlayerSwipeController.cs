using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipeController : MonoBehaviour
{
    Vector2 touchDown;
    Vector2 touchUp;

    void Update()
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
            IListenUpSwipe[] swipeComponents = this.GetComponents<IListenUpSwipe>();
            foreach (IListenUpSwipe component in swipeComponents)
            {
                component.SwipedUp();
            }
        }
        else if (touchDelta.y < 0 && touchDelta.x > -0.5f && touchDelta.x < 0.5f)
        {
            IListenDownSwipe[] swipeComponents = this.GetComponents<IListenDownSwipe>();
            foreach (IListenDownSwipe component in swipeComponents)
            {
                component.SwipedDown();
            }
        }
        else if (touchDelta.x > 0 && touchDelta.y > -0.5f && touchDelta.y < 0.5f)
        {
            IListenRightSwipe[] swipeComponents = this.GetComponents<IListenRightSwipe>();
            foreach (IListenRightSwipe component in swipeComponents)
            {
                component.SwipedRight();
            }
        }
        else if (touchDelta.x < 0 && touchDelta.y > -0.5f && touchDelta.y < 0.5f)
        {
            IListenLeftSwipe[] swipeComponents = this.GetComponents<IListenLeftSwipe>();
            foreach (IListenLeftSwipe component in swipeComponents)
            {
                component.SwipedLeft();
            }
        }
    }
}
