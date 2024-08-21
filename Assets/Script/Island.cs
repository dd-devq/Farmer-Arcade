using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public bool IsDead;
    public float WaterResource;
    public bool IsPond;
    public float CorrosionSpeed;

    void Start()
    {
        if (IsDead)
        {

        }
    }

    void Update()
    {
        if (!IsPond)
        {
            WaterResource -= CorrosionSpeed * Time.deltaTime;
        }
    }

}
