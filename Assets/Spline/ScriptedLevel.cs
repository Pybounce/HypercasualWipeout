using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/ScriptedLevel")]
public class ScriptedLevel : ScriptableObject
{
    public BezierPoint[] bezierCurve;
}

[Serializable]
public struct BezierPoint
{
    public Vector3 anchorPosition;
    public float controlRotation;
    public float controlScale;
}

