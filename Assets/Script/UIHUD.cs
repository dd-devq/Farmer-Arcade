using TMPro;
using UnityEngine;

public class UIHUD : BaseUI
{
    public TextMeshProUGUI WoodAmount;
    public TextMeshProUGUI WaterAmount;

    public GameObject PopUpPrefab;

    public void PopUpWood(int amount, bool isAdd)
    {
        // Prefab
        WoodAmount.SetText(ResourceManager.Instance.WoodResource.ToString());
    }

    public void PopUpWater(int amount, bool isAdd)
    {
        // Prefab
        WoodAmount.SetText(ResourceManager.Instance.WaterResource.ToString());
    }
}
