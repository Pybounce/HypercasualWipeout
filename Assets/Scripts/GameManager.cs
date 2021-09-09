using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SplineCreator levelSpline;
    private Scripted2DSpline spline2D;
    private ScriptedLevel level;
    [SerializeField] private float splineSpacing = 2f;
    
    private void Awake()
    {
        GetLevelSettings();
        CreateSpline();
        CreateProps();
    }
    private void GetLevelSettings()
    {
        LevelSettings levelSettingsObj = GameObject.FindObjectOfType<LevelSettings>();
        this.level = levelSettingsObj.GetLevel();
        this.spline2D = levelSettingsObj.GetSpline();
    }
    private void CreateSpline()
    {
        levelSpline = new SplineCreator(spline2D, level, splineSpacing);
        GameObject path = new GameObject();
        path.AddComponent<MeshFilter>();
        path.AddComponent<MeshRenderer>();
        path.GetComponent<MeshFilter>().mesh = levelSpline.GetSplineMesh();
        path.GetComponent<MeshRenderer>().sharedMaterial = spline2D.material;
    }
    private void CreateProps()
    {
        PropCreator propCreator = new PropCreator(level.propData, levelSpline.GetEvenlySpacedPoints());
        propCreator.CreateProps();
    }

    public SplineCreator GetSpline()
    {
        return levelSpline;
    }

}
