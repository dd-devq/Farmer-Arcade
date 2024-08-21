using UnityEngine;

public class ResourceManager : SingletonMono<ResourceManager>
{
    public int WoodResource;
    public int WaterResource;

    public UIHUD HUD;

    void Start()
    {
        WoodResource = 0;
        WaterResource = 0;

        HUD.WoodAmount.SetText("0");
        HUD.WaterAmount.SetText("0");
    }

    public void AddWood(int amount)
    {
        WoodResource += amount;
        HUD.PopUpWood(WoodResource, true);
    }

    public void AddWater(int amount)
    {
        WaterResource += amount;
        HUD.PopUpWater(WaterResource, true);
    }

    public void SubstractWood(int amount)
    {
        WoodResource -= amount;
        HUD.PopUpWood(WoodResource, false);
    }

    public void SubstractWater(int amount)
    {
        WaterResource -= amount;
        HUD.PopUpWater(WaterResource, false);
    }
}
