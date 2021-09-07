using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Scripted2DSpline spline;
    public ScriptedLevel level;

    private void Start()
    {
        SplineCreator newSpline = new SplineCreator(spline, level, 1f);
        Mesh splineMesh = newSpline.GetSplineMesh();
        GameObject splineObject = new GameObject();
        splineObject.AddComponent<MeshFilter>();
        splineObject.AddComponent<MeshRenderer>();
        splineObject.GetComponent<MeshFilter>().mesh = splineMesh;

    }
}
