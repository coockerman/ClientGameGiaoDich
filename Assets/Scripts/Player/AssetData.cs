using System.Collections.Generic;
using UnityEngine.Serialization;

public class AssetData
{
    public int countMoney { get; set; }
    public List<ComboItem> assets { get; set; }

    public AssetData()
    {
        
    }

    public AssetData(int countMoney, List<ComboItem> assets)
    {
        this.countMoney = countMoney;
        this.assets = assets;
    }

    public int GetAssetCountByType(string type)
    {
        var item = assets.Find(a => a.type == type);
        return item != null ? item.count : 0;
    }

    public void SetAssetCountByType(string type, int count)
    {
        var item = assets.Find(a => a.type == type);
        if (item != null)
        {
            item.count = count;
        }
    }
}
[System.Serializable]
public class ComboItem
{
    public string type;
    public int count;

    public ComboItem(string type, int count)
    {
        this.type = type;
        this.count = count;
    }
}
