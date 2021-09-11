using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMovementSlide : MonoBehaviour, IListenDownSwipe
{
    private PlayerController playerController;

    [SerializeField] private float targetHeight = 1f;
    [SerializeField] private float slideTime = 0.5f;
    [SerializeField] private float duckSpeed = 2f;
    private float originalHeight;

    private void Awake()
    {
        playerController = this.GetComponent<PlayerController>();
        originalHeight = GetCurrentHeight();
    }
    
    public void SwipedDown()
    {
        if (playerController.GetState(PlayerState.Jumping) == false && playerController.GetState(PlayerState.Sliding) == false)
        {
            playerController.SetState(PlayerState.Sliding, true);
            StartCoroutine(SlideDown());
        }
    }
    
    private IEnumerator SlideDown()
    {
        while (targetHeight < GetCurrentHeight())
        {
            float duckDelta = duckSpeed * Time.deltaTime;
            float heightDelta = GetCurrentHeight() - targetHeight;
            float heightChange = Mathf.Min(duckDelta, heightDelta);
            this.transform.localScale -= new Vector3(0f, heightChange, 0f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(slideTime);
        StartCoroutine(SlideUp());
    }
    private IEnumerator SlideUp()
    {
        while (GetCurrentHeight() < originalHeight)
        {
            float duckDelta = duckSpeed * Time.deltaTime;
            float heightDelta = originalHeight - GetCurrentHeight();
            float heightChange = Mathf.Min(duckDelta, heightDelta);
            this.transform.localScale += new Vector3(0f, heightChange, 0f);
            yield return null;
        }
        playerController.SetState(PlayerState.Sliding, false);

    }
 

    private float GetCurrentHeight()
    {
        return this.transform.localScale.y;
    }
}
