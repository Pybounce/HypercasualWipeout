using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private ScriptedLevel level;

    
    public void LevelSelected()
    {
        if (level != null)
        {
            LoadLevel();
        }
        else
        {
            Debug.LogWarning("Level button has null reference");
        }
    }
    private void LoadLevel()
    {
        GameObject levelSettingsObject = new GameObject();
        levelSettingsObject.name = "LevelSettingsObject";
        levelSettingsObject.AddComponent<LevelSettings>().Init(level);
        StartCoroutine(PybUtilityScene.LoadScene("GameScene"));
    }
}
