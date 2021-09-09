using UnityEngine;

[RequireComponent(typeof(Transform))]
public class BasicMovement : MonoBehaviour
{
    [SerializeField] private Vector3 rotSpeed;
    [SerializeField] private Vector3 startRot;

    [SerializeField] private AnimationCurve yMoveCurve;
    [SerializeField] private bool useYMove = false;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(startRot);
    }
    private void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);
        if (useYMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (yMoveCurve.Evaluate(Time.time) * Time.deltaTime), transform.position.z);
        }
    }
}