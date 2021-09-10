using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private ScriptedLevel level;
    [SerializeField] private Scripted2DSpline spline2D;

    
    public void LevelSelected()
    {
        if (level != null && spline2D != null)
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
        SceneManager.LoadScene("GameScene");
    }
}
