using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private ScriptedLevel level;
    [SerializeField] private Scripted2DSpline spline2D;

    public void LevelSelected()
    {
        GameObject levelSettingsObject = new GameObject();
        levelSettingsObject.name = "LevelSettingsObject";
        levelSettingsObject.AddComponent<LevelSettings>();
        levelSettingsObject.GetComponent<LevelSettings>().SetSettings(level, spline2D);
        SceneManager.LoadScene("GameScene");
    }
}
