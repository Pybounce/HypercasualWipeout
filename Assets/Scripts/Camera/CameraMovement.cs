using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform lookTransform;
    [SerializeField] private Vector3 lookDelta;
    [SerializeField] private Transform cameraFollowTransform;
    [SerializeField] private float speed = 2f;
    private bool dynamicSpeed = true;

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        float newSpeed = speed;
        if (dynamicSpeed)
        {
            //Camera should speed up when further away from the target
            float maxDistance = 1f;
            float d = Vector3.Distance(this.transform.position, cameraFollowTransform.position);
            newSpeed = speed + ((d * d * 2) - maxDistance);
        }
        Vector3 targetDelta = cameraFollowTransform.position - this.transform.position;
        targetDelta.Normalize();
        this.transform.position += targetDelta * newSpeed * Time.deltaTime;
        this.transform.LookAt(lookTransform.position + lookDelta);
    }


    public void SetFollowTransform(Transform _pos)
    {
        this.cameraFollowTransform = _pos;
    }
    public void SetLookTransform(Transform _lookTransform)
    {
        this.lookTransform = _lookTransform;
    }
    public void SetLookDelta(Vector3 _delta)
    {
        this.lookDelta = _delta;
    }
    public void SetSpeed(float _speed)
    {
        this.speed = _speed;
    }
    public void SetDynamicSpeed(bool _isDynamic)
    {
        this.dynamicSpeed = _isDynamic;
    }

}
