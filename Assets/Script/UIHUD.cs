using TMPro;
using UnityEngine;

public class UIHUD : BaseUI
{
    public TextMeshProUGUI WoodAmount;
    public TextMeshProUGUI WaterAmount;
    public TextMeshProUGUI IslandAmount;
    public GameObject PopUpPrefab;

    private void Start()
    {
        WoodAmount.SetText(ResourceManager.Instance.WoodResource.ToString());
        WaterAmount.SetText(ResourceManager.Instance.WaterResource.ToString());
        IslandAmount.SetText(ResourceManager.Instance.LiveIsland + "/" + ResourceManager.Instance.IslandAmount + " Islands");
    }


    public void PopUpWood(int amount, bool isAdd)
    {
        // Prefab
        WoodAmount.SetText(ResourceManager.Instance.WoodResource.ToString());
    }

    public void PopUpWater(int amount, bool isAdd)
    {
        // Prefab
        WaterAmount.SetText(ResourceManager.Instance.WaterResource.ToString());
    }

    public void UpdateIsland()
    {
        IslandAmount.SetText(ResourceManager.Instance.LiveIsland + "/" + ResourceManager.Instance.IslandAmount + " Islands");
    }
}
