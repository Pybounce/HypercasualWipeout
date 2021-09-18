using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IListenUpSwipe[] swipeComponents = this.GetComponents<IListenUpSwipe>();
            foreach (IListenUpSwipe component in swipeComponents)
            {
                component.SwipedUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            IListenDownSwipe[] swipeComponents = this.GetComponents<IListenDownSwipe>();
            foreach (IListenDownSwipe component in swipeComponents)
            {
                component.SwipedDown();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            IListenRightSwipe[] swipeComponents = this.GetComponents<IListenRightSwipe>();
            foreach (IListenRightSwipe component in swipeComponents)
            {
                component.SwipedRight();
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            IListenLeftSwipe[] swipeComponents = this.GetComponents<IListenLeftSwipe>();
            foreach (IListenLeftSwipe component in swipeComponents)
            {
                component.SwipedLeft();
            }
        }
    }
}
