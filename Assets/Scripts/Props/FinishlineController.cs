using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishlineController : MonoBehaviour
{
    private CameraMovement cameraMovement;
    [SerializeField] private Transform endingCameraTransform;
    [SerializeField] private List<ParticleSystem> confettiCannons = new List<ParticleSystem>();

    private void Awake()
    {
        cameraMovement = FindObjectOfType<CameraMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cameraMovement.SetFollowTransform(endingCameraTransform);
            cameraMovement.SetLookTransform(this.transform);
            cameraMovement.SetLookDelta(new Vector3(0, 3f, 0));
            cameraMovement.SetDynamicSpeed(false);
            foreach (ParticleSystem confetti in confettiCannons)
            {
                confetti.Play();
            }
            StartCoroutine(PybUtilityScene.LoadScene("LevelSelectScene", 6f));
        }
    }
}
