using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SplineCreator levelSpline;
    [SerializeField] private Scripted2DSpline spline2D;
    [SerializeField] private ScriptedLevel level;
    [SerializeField] private float splineSpacing = 2f;
    
    private void Awake()
    {
        CreateSpline();
    }

    private void CreateSpline()
    {
        levelSpline = new SplineCreator(spline2D, level, splineSpacing);
        GameObject path = new GameObject();
        path.AddComponent<MeshFilter>();
        path.AddComponent<MeshRenderer>();
        path.GetComponent<MeshFilter>().mesh = levelSpline.GetSplineMesh();
    }

    public SplineCreator GetSpline()
    {
        return levelSpline;
    }

}
