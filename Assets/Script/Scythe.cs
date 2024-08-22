using System.Collections;
using UnityEngine;

public class Scythe : MonoBehaviour
{

    public void ScytheCallback(IEnumerator callback)
    {
        StartCoroutine(callback);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            AudioManager.Instance.PlaySound("chop");
        }
    }

}
