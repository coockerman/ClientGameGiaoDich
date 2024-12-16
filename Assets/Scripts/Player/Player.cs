using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player instance;
    private AssetPlayer assetPlayer;
    private string namePlayer;
    private int day = 1;
    public int Day { get { return day; } set { day = value; } }
    public string NamePlayer {get {return namePlayer; }}
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
        assetPlayer = new AssetPlayer(10000, initialResources, solierResources); // 100000 là số tiền khởi đầu
    }

    private void Start()
    {
    }

    public void UpDayPlayer()
    {
        day += 1;
        UIManager.instance.uiInformation.UpdateDayPlayer(day);
    }
    public void SetupNamePlayer(string namePlayer)
    {
        UIManager.instance.uiRegisterName.gameObject.SetActive(false);
        this.namePlayer = namePlayer;
        UIManager.instance.uiInformation.Init(namePlayer, day);
    }
    public bool CheckAddAsset(ItemType itemType, float price, float amount)
    {
        bool result = assetPlayer.AddItem(itemType, price, amount);
        return result;
    }
    public bool CheckRemoveAsset(ItemType itemType, float price, float amount)
    {
        bool result = assetPlayer.RemoveItem(itemType, price, amount);
        return result;
    }
    public bool CheckAddAsset(SolierType solierType, float amount)
    {
        bool result = assetPlayer.AddSolider(solierType, amount);
        return result;
    }
    public bool CheckRemoveAsset(SolierType solierType, float amount)
    {
        bool result = assetPlayer.RemoveSolider(solierType, amount);
        return result;
    }

    public float GetResourceAmount(ItemType itemType)
    {
        return assetPlayer.GetResourceAmount(itemType);
    }

    public float GetSolierAmount(SolierType solierType)
    {
        return assetPlayer.GetSolierAmount(solierType);
    }

    public float GetMoneyAmount()
    {
        return assetPlayer.Money;
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
        if (price < 0 || amount <= 0 || money < price * amount) return false;

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
        UIManager.instance.UpdateUIViewItems();
        return true;
    }

    public bool RemoveItem(ItemType itemType, float price, float amount)
    {
        if (price < 0 || amount <= 0) return false;

        // Kiểm tra tồn tại tài nguyên và đủ số lượng để bán
        if (resources.ContainsKey(itemType) && resources[itemType] >= amount)
        {
            resources[itemType] -= amount;
            money += price * amount;
            UIManager.instance.UpdateUIViewItems();
            return true;
        }
        return false;
    }
    public bool AddSolider(SolierType solierType, float amount)
    {
        // Trừ tiền và thêm tài nguyên
        if (soliers.ContainsKey(solierType))
        {
            soliers[solierType] += amount;
        }
        else
        {
            soliers[solierType] = amount; // Nếu tài nguyên chưa tồn tại
        }
        UIManager.instance.UpdateUIViewItems();
        return true;
    }

    public bool RemoveSolider(SolierType solierType, float amount)
    {
        if (soliers.ContainsKey(solierType))
        {
            if (soliers[solierType] >= amount)
            {
                soliers[solierType] -= amount;
                UIManager.instance.UpdateUIViewItems();
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public float GetResourceAmount(ItemType itemType)
    {
        return resources.ContainsKey(itemType) ? resources[itemType] : 0f;
    }

    public float GetSolierAmount(SolierType solierType)
    {
        return soliers.ContainsKey(solierType) ? soliers[solierType] : 0f;
    }
    public override string ToString()
    {
        string resourceInfo = string.Join(", ", resources.Select(r => $"{r.Key}: {r.Value}"));
        return $"Money: {money}, Resources: [{resourceInfo}]";
    }
}

