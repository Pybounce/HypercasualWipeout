using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    private ScriptedLevel level;
    private Scripted2DSpline spline2D;

   
    public void SetSettings(ScriptedLevel _level, Scripted2DSpline _spline2D)
    {
        level = _level;
        spline2D = _spline2D;
    }
    public ScriptedLevel GetLevel()
    {
        return this.level;
    }
    public Scripted2DSpline GetSpline()
    {
        return this.spline2D;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
