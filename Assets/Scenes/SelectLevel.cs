using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    public void LevelSelected()
    {
        
        SceneManager.LoadScene("SampleScene");
    }
}
