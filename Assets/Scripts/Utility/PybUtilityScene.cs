using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class PybUtilityScene
{
    public static IEnumerator ReloadScene(float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static IEnumerator LoadScene(string sceneName, float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }
    
}
