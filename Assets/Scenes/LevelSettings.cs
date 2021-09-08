using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    private ScriptedLevel level;
    private Scripted2DSpline spline2D;

    public LevelSettings(ScriptedLevel _level, Scripted2DSpline _spline2D)
    {
        level = _level;
        spline2D = _spline2D;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

}
