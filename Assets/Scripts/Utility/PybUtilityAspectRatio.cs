using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PybUtilityAspectRatio : MonoBehaviour
{
    [SerializeField] private Vector2 aspectRatio = Vector2.one;

    private void Awake()
    {
        Screen.SetResolution((int)(Screen.height * (aspectRatio.x / aspectRatio.y)), Screen.height, true);
    }
}
