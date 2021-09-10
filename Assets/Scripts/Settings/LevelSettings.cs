using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    private ScriptedLevel level;
    private bool hasInitialised = false;

   
    public void Init(ScriptedLevel _level)
    {
        if (!hasInitialised)
        {
            level = _level;
            hasInitialised = true;
        }
        
    }
    public ScriptedLevel GetLevel()
    {
        return this.level;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
