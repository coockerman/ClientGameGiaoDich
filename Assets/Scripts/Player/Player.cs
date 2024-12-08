using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player instance;
    private AssetPlayer assetPlayer;

    private void Awake()
    {
        instance = this;

        // Khởi tạo tài nguyên ban đầu cho người chơi
        var initialResources = new Dictionary<ItemType, float>
        {
            { ItemType.Gold, 50 },
            { ItemType.Iron, 100 },
            { ItemType.Food, 200 }
        };
        var solierResources = new Dictionary<SolierType, float>
        {
            { SolierType.Melee, 0 },
            { SolierType.Arrow, 0 },
            { SolierType.Cavalry, 0 },
            { SolierType.Citizen, 0 }
        };
        assetPlayer = new AssetPlayer(100000, initialResources, solierResources); // 100000 là số tiền khởi đầu
    }

    public bool CheckAddAsset(ItemType itemType, float price, float amount)
    {
        bool result = assetPlayer.AddItem(itemType, price, amount);
        return result;
    }

    public bool CheckRemoveAsset(ItemType itemType, float price, float amount)
    {
        bool result = assetPlayer.SellItem(itemType, price, amount);
        return result;
    }

    public float GetResourceAmount(ItemType itemType)
    {
        return assetPlayer.GetResourceAmount(itemType);
    }
}




public class AssetPlayer
{
    private float money; // Tiền của người chơi
    private Dictionary<ItemType, float> resources; // Lưu trữ tài nguyên
    private Dictionary<SolierType, float> soliers;
    public AssetPlayer(float money, Dictionary<ItemType, float> initialResources, Dictionary<SolierType, float> soliderResources)
    {
        this.money = money;
        this.resources = new Dictionary<ItemType, float>(initialResources);
        this.soliers = new Dictionary<SolierType, float>(soliderResources);
    }

    public float Money => money;

    public bool AddItem(ItemType itemType, float price, float amount)
    {
        if (price <= 0 || amount <= 0 || money < price * amount) return false;

        // Trừ tiền và thêm tài nguyên
        money -= price * amount;
        if (resources.ContainsKey(itemType))
        {
            resources[itemType] += amount;
        }
        else
        {
            resources[itemType] = amount; // Nếu tài nguyên chưa tồn tại
        }
        return true;
    }

    public bool SellItem(ItemType itemType, float price, float amount)
    {
        if (price <= 0 || amount <= 0) return false;

        // Kiểm tra tồn tại tài nguyên và đủ số lượng để bán
        if (resources.ContainsKey(itemType) && resources[itemType] >= amount)
        {
            resources[itemType] -= amount;
            money += price * amount;
            return true;
        }
        return false;
    }

    public float GetResourceAmount(ItemType itemType)
    {
        return resources.ContainsKey(itemType) ? resources[itemType] : 0f;
    }

    public override string ToString()
    {
        string resourceInfo = string.Join(", ", resources.Select(r => $"{r.Key}: {r.Value}"));
        return $"Money: {money}, Resources: [{resourceInfo}]";
    }
}

