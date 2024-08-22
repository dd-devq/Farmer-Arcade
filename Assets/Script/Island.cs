using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Island : MonoBehaviour
{
    public bool IsDead;
    public int WaterResource;
    public int WaterAbsorbed;
    public bool IsPond;
    public int CorrosionSpeed;
    private AudioSource _audioSource;

    public TextMeshProUGUI WaterIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        WaterAbsorbed = 0;
        if (IsDead)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Contains("Island") || child.gameObject.name.Contains("Water"))
                {
                    continue;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        WaterIndex.SetText(WaterAbsorbed + "/" + WaterResource);
        InvokeRepeating(nameof(DrainWater), 0.0f, 7f);
    }
    public void DrainWater()
    {
        if (WaterAbsorbed == 0)
        {
            return;
        }

        WaterAbsorbed -= CorrosionSpeed;
        WaterIndex.SetText(WaterAbsorbed + "/" + WaterResource);

        if (WaterAbsorbed == 0)
        {
            DestroyIsland();
        }
    }
    public void ExtractWater()
    {
        var waterAmount = WaterResource - WaterAbsorbed;
        if (ResourceManager.Instance.WaterResource >= waterAmount)
        {
            ResourceManager.Instance.SubstractWater(waterAmount);
            WaterAbsorbed = WaterResource;
        }
        else
        {
            WaterAbsorbed += ResourceManager.Instance.WaterResource;
            ResourceManager.Instance.SubstractWater(ResourceManager.Instance.WaterResource);
        }
        WaterIndex.SetText(WaterAbsorbed + "/" + WaterResource);

        if (WaterResource == WaterAbsorbed)
        {
            ReviveIsland();
            _audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && WaterAbsorbed < WaterResource)
        {
            ExtractWater();
        }
    }

    public void ReviveIsland()
    {
        ResourceManager.Instance.AddIsland(1);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void DestroyIsland()
    {
        IsDead = true;
        ResourceManager.Instance.SubstractIsland(1);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Contains("Island") || child.gameObject.name.Contains("Water"))
            {
                continue;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
