using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player instance;
    private string username;
    private string usernameWaitRegister;
    [SerializeField] private float countGoldDefault = 10;
    [SerializeField] private float countIronDefault = 50;
    [SerializeField] private float countFoodDefault = 350;
    [SerializeField] private float countMoneyDefault = 1000;
    
    private AssetPlayer assetPlayer;
    
    private string namePlayer;
    private string namePlayerWaitRegister;
    
    private int day = 1;
    
    public string Username => username;
    public string NamePlayer => namePlayer;

    public string UsernameWaitRegister { get => usernameWaitRegister; set => usernameWaitRegister = value; }
    public string NamePlayerWaitRegister { get => namePlayerWaitRegister; set => namePlayerWaitRegister = value; }
    
    public int Day => day;

    private void Awake()
    {
        instance = this;
    }

    public void InitUsername()
    {
        username = usernameWaitRegister;
    }

    public void InitNamePlayer()
    {
        namePlayer = namePlayerWaitRegister;
    }
    public void InitResourcePlayer()
    {
        // Khởi tạo tài nguyên ban đầu cho người chơi
        var initialResources = new Dictionary<ItemType, float>
        {
            { ItemType.Gold, countGoldDefault },
            { ItemType.Iron, countIronDefault },
            { ItemType.Food, countFoodDefault }
        };
        var solierResources = new Dictionary<SolierType, float>
        {
            { SolierType.Melee, 0 },
            { SolierType.Arrow, 0 },
            { SolierType.Cavalry, 0 },
            { SolierType.Citizen, 0 }
        };
        assetPlayer = new AssetPlayer(countMoneyDefault, initialResources, solierResources); // 10000 là số tiền khởi đầu
    }
    public void UpDayPlayer()
    {
        day += 1;
        UIManager.instance.uiInformation.UpdateDayPlayer(day);
        UIManager.instance.UpdateUIViewItems();
        SoundManager.instance.PlayDaySound();
        
        SoldierData soldierData = new SoldierData(
            assetPlayer.GetSolierAmount(SolierType.Melee),
            assetPlayer.GetSolierAmount(SolierType.Arrow),
            assetPlayer.GetSolierAmount(SolierType.Cavalry)
            );
        Debug.Log(soldierData.Melee + " " + soldierData.Arrow + " " +soldierData.Cavalry);
        GameManager.instance.RequestDayPlay(day, soldierData);
    }
    public void SetupNamePlayer(string namePlayer)
    {
        this.namePlayer = namePlayer;
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

    public void AddMoneyAmount(float amount)
    {
        assetPlayer.AddMoney(amount);
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

    public void AddMoney(float amount)
    {
        UIManager.instance.UpdateUIViewItems();
        money += amount;
    }

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

