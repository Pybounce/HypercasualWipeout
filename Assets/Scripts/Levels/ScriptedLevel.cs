using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/ScriptedLevel")]
public class ScriptedLevel : ScriptableObject
{
    public BezierPoint[] bezierCurve;
    public PropData[] propData;
}

[Serializable]
public struct BezierPoint
{
    public Vector3 anchorPosition;
    public float controlRotation;
    public float controlScale;
}

[Serializable]
public struct PropData
{
    public GameObject propObject;
    /// <summary>
    /// X = even point index, Y = lane index --
    /// If it exceeds the total even point count, the obstacle will be ignored, and a warning is thrown.
    /// </summary>
    public Vector2Int[] positions;
}

