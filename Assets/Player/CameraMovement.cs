using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraFollowObject;
    [SerializeField] private float speed = 2f;

    
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //Camera should speed up when further away from the target
        float maxDistance = 1f;
        float d = Vector3.Distance(this.transform.position, cameraFollowObject.position);
        float s = speed + ((d * d * 2) - maxDistance);
        Vector3 targetDelta = cameraFollowObject.position - this.transform.position;
        targetDelta.Normalize();
        this.transform.position += targetDelta * s * Time.deltaTime;
        this.transform.LookAt(player);
    }

}
