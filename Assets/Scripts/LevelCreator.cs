using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public ScriptedLevel level;
    private static GameObject[] splines;
    public SplineCreator splineCreator;

    public void LoadSplines()
    {
        ClearSplines();
        CheckCurvePoints();
        splineCreator = new SplineCreator(level, level.spacing);
        splines = new GameObject[level.splineTemplates.Length];
        for (int spline = 0; spline < level.splineTemplates.Length; spline++)
        {
            GameObject newSplineObject = new GameObject();
            newSplineObject.AddComponent<MeshFilter>();
            newSplineObject.AddComponent<MeshRenderer>();
            newSplineObject.GetComponent<MeshFilter>().mesh = splineCreator.GetSplineMesh(level.splineTemplates[spline]);
            newSplineObject.GetComponent<MeshRenderer>().sharedMaterial = level.splineTemplates[spline].material;
            splines[spline] = newSplineObject;
        }
    }

    private void CheckCurvePoints()
    {
        if (level.curvePoints.Length < 4)
        {
            Vector3[] defaultPoints =
            {
                new Vector3(0,0,0),
                new Vector3(0, 0, 10),
                new Vector3(0, 0, 30),
                new Vector3(0, 0, 40)
            };
            level.curvePoints = defaultPoints;

        }

    }

    private void ClearSplines()
    {
        if (splines != null)
        {
            foreach (GameObject spline in splines)
            {
                DestroyImmediate(spline);
            }
        }
    }
    
    public void ClearAll()
    {
        ClearSplines();
        splineCreator = null;
    }

}
