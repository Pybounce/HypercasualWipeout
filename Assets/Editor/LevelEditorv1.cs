using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptedLevel))]
public class LevelEditor : Editor
{
    ScriptedLevel level;
    SplineCreator splineCreator;
    public static List<GameObject> splines;

    private void OnEnable()
    {
        level = target as ScriptedLevel;
        if (splines == null)
        {
            splines = new List<GameObject>();
        }
        SceneView.duringSceneGui += CustomOnSceneGUI;
    }
    private void OnDisable()
    {
        DestroySplines();
        SceneView.duringSceneGui -= CustomOnSceneGUI;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        using (new EditorGUI.DisabledScope(PybUtilityScene.GetCurrentScene().name != "LevelEditorScene"))
        {
            if (GUILayout.Button("Update Curve") == true)
            {
                UpdateCurve();
            }

        }
    }

    public void CustomOnSceneGUI(SceneView _sceneView)
    {
        if (splineCreator != null && splineCreator.GetCurvePoints().Length > 0)
        {
            Vector3[] points = splineCreator.GetCurvePoints();
            Color[] pointColours = { Color.red, Color.green, Color.blue };
            int colourIndex = 0;
            for (int i = 0; i < points.Length; i++)
            {
                float sphereSize = 1.3f;
                if (i % 3 == 0)
                {
                    Handles.color = Color.white;
                    if (i > 0) { Handles.DrawAAPolyLine(new Vector3[] { points[i], points[i - 1] }); }
                    if (i < points.Length - 1) { Handles.DrawAAPolyLine(new Vector3[] { points[i], points[i + 1] }); }
                    sphereSize = 1.8f;
                }
                Handles.color = pointColours[colourIndex];
                Handles.SphereHandleCap(0, points[i], Quaternion.identity, sphereSize, EventType.Repaint);
                points[i] = Handles.PositionHandle(points[i], Quaternion.identity);
                colourIndex += 1;
                if (colourIndex >= pointColours.Length) { colourIndex -= pointColours.Length; }
            }
        }
    }

    private void DestroySplines()
    {
        for (int spline = 0; spline < splines.Count; spline++)
        {
            DestroyImmediate(splines[spline]);
        }
    }
    private void UpdateCurve()
    {
        
        splineCreator = new SplineCreator(level, level.spacing);
        DestroySplines();
        for (int spline = 0; spline < level.splineTemplates.Length; spline++)
        {
            GameObject newSplineObject = new GameObject();
            newSplineObject.AddComponent<MeshFilter>();
            newSplineObject.AddComponent<MeshRenderer>();
            newSplineObject.GetComponent<MeshFilter>().mesh = splineCreator.GetSplineMesh(level.splineTemplates[spline]);
            newSplineObject.GetComponent<MeshRenderer>().sharedMaterial = level.splineTemplates[spline].material;
            splines.Add(newSplineObject);
        }
    }
}
