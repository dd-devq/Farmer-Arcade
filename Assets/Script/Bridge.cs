using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int WoodResource;
    public int WoodAbsorbed;
    private AudioSource _audioSource;
    public TextMeshProUGUI WoodResourceText;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
        WoodResourceText.SetText(WoodAbsorbed + "/" + WoodResource);
    }

    public void ExtractWood()
    {
        var woodAmount = WoodResource - WoodAbsorbed;
        if (ResourceManager.Instance.WoodResource >= woodAmount)
        {
            ResourceManager.Instance.SubstractWood(woodAmount);
            WoodAbsorbed = WoodResource;

        }
        else
        {
            WoodAbsorbed += ResourceManager.Instance.WoodResource;
            ResourceManager.Instance.SubstractWood(ResourceManager.Instance.WoodResource);
        }
        WoodResourceText.SetText(WoodAbsorbed + "/" + WoodResource);

        if (WoodResource == WoodAbsorbed)
        {
            _audioSource.Play();
            StartCoroutine(WaitForSound());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && WoodAbsorbed < WoodResource)
        {
            ExtractWood();
        }
    }

    public IEnumerator WaitForSound()
    {
        yield return new WaitUntil(() => _audioSource.isPlaying == false);
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
