using System;
using UnityEngine;

[CreateAssetMenu(fileName = "2DSpline_", menuName = "ScriptableObjects/2DSpline")]
public class Scripted2DSpline : ScriptableObject
{
    public vert2D[] verts;
    public Vector2Int[] vertPairs;
}

[Serializable]
public struct vert2D
{
    public Vector2 position;
    public Vector2 uv;
    public Vector2 normal;
}