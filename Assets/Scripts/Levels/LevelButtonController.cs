using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    
    public void LevelSelected()
    {
        LoadLevel(levelIndex);
    }
    private void LoadLevel(int _levelIndex)
    {
        GameObject levelSettingsObject = new GameObject();
        levelSettingsObject.name = "LevelSettingsObject";
        levelSettingsObject.AddComponent<LevelManager>().LoadLevel(_levelIndex);
    }
}
