using System.Collections.Generic;

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
        var item = assets.Find(a => a.Type == type);
        return item != null ? item.Count : 0;
    }

    public void SetAssetCountByType(string type, int count)
    {
        var item = assets.Find(a => a.Type == type);
        if (item != null)
        {
            item.Count = count;
        }
    }
}
public class ComboItem
{
    public string Type { get; set; }
    public int Count { get; set; }

    public ComboItem(string type, int count)
    {
        Type = type;
        Count = count;
    }
}
