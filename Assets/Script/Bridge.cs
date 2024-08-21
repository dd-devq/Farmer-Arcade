using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int WoodResource;
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ExtractWood()
    {
        if (ResourceManager.Instance.WoodResource >= WoodResource)
        {
            ResourceManager.Instance.WoodResource -= WoodResource;
            WoodResource = 0;

        }
        else
        {
            WoodResource -= ResourceManager.Instance.WoodResource;
            ResourceManager.Instance.WoodResource = 0;
        }

        if (WoodResource == 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}
