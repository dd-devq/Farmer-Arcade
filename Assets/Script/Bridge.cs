using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int WoodResource;
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "BuildZone")
            {
                continue;
            }
            child.gameObject.SetActive(false);
        }
    }

    public void ExtractWood()
    {
        if (ResourceManager.Instance.WoodResource >= WoodResource)
        {
            ResourceManager.Instance.SubstractWood(WoodResource);
            WoodResource = 0;

        }
        else
        {
            WoodResource -= ResourceManager.Instance.WoodResource;
            ResourceManager.Instance.SubstractWood(ResourceManager.Instance.WoodResource);
        }

        if (WoodResource == 0)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "BuildZone")
                {
                    child.gameObject.SetActive(false);
                    continue;
                }
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            ExtractWood();
        }
    }

}
