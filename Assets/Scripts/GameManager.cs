using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SplineCreator levelSpline;
    private ScriptedLevel level;
    
    private void Awake()
    {
        RetrieveLevelSettings();
        CreateSpline();
        CreateProps();
    }
    private void RetrieveLevelSettings()
    {
        LevelManager levelSettingsObj = GameObject.FindObjectOfType<LevelManager>();
        this.level = levelSettingsObj.GetLevel();
    }
    private void CreateSpline()
    {
        levelSpline = new SplineCreator(level, level.spacing);
        
        for (int spline = 0; spline < level.splineTemplates.Length; spline++)
        {
            GameObject newSplineObject = new GameObject();
            newSplineObject.AddComponent<MeshFilter>();
            newSplineObject.AddComponent<MeshRenderer>();
            newSplineObject.GetComponent<MeshFilter>().mesh = levelSpline.GetSplineMesh(level.splineTemplates[spline]);
            newSplineObject.GetComponent<MeshRenderer>().sharedMaterial = level.splineTemplates[spline].material;
        }
        
    }
    private void CreateProps()
    {
        PropCreator propCreator = new PropCreator(level.propData, levelSpline.GetEvenlySpacedPoints(), level.laneWidth, level.laneCount);
        propCreator.CreateProps();
    }

    public SplineCreator GetSpline()
    {
        return levelSpline;
    }

}
