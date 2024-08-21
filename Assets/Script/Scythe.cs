using System;
using System.Collections;
using UnityEngine;

public class Scythe : MonoBehaviour
{

    public void ScytheCallback(IEnumerator callback)
    {
        StartCoroutine(callback);
    }

}
