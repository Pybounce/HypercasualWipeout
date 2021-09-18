using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/ScriptedLevel")]
public class ScriptedLevel : ScriptableObject
{
    public int laneCount;
    public float laneWidth;
    public float spacing;
    public Scripted2DSpline[] splineTemplates;
    public Vector3[] curvePoints;
    public PropData[] propData;
}


[Serializable]
public struct PropData
{
    public GameObject propObject;
    public PropClusterData[] clusterData;
    [Serializable]
    public struct PropClusterData
    {
        public int startIndex;
        public int interval;
        public int amount;
        public float horizontalPosition;
        /// <summary>
        /// This will multiply the potiion by the lane width, so right lane = 1, left = -1, mid = 0 etc
        /// </summary>
        public bool useLaneWidth;
    }
}

