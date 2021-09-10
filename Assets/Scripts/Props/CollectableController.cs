using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Collected());
        }
    }

    private IEnumerator Collected()
    {
        this.GetComponent<BasicMovement>().enabled = false;
        float timePassed = 0f;
        while (timePassed < 1f)
        {
            transform.localPosition += Vector3.up * Time.deltaTime * 8f;
            transform.localScale -= Vector3.one * Time.deltaTime;
            transform.Rotate(Vector3.up * Time.deltaTime * 1000);
            timePassed += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
