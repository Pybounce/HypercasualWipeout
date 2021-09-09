using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            this.transform.SetParent(null);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<PlayerMovement>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(ReloadLevel(2f));
            
        }
    }

    private IEnumerator ReloadLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
