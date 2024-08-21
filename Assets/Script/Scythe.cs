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
        AudioManager.Instance.PlaySound("chop");
    }

}
