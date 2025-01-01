using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static Player instance;
    private string username;
    private string usernameWaitRegister;
    
    private string namePlayer;
    private string namePlayerWaitRegister;
    
    private int day = 0;
    private AssetData assetData;
    public string Username => username;
    public string NamePlayer => namePlayer;

    public string UsernameWaitRegister { get => usernameWaitRegister; set => usernameWaitRegister = value; }
    public string NamePlayerWaitRegister { get => namePlayerWaitRegister; set => namePlayerWaitRegister = value; }
    
    public int Day => day;
    public Dictionary<ItemType, int> initialResources = new Dictionary<ItemType, int>();
    
    private AssetPlayer assetPlayer;
    [SerializeField] private float countGoldDefault = 10;
    [SerializeField] private float countIronDefault = 50;
    [SerializeField] private float countFoodDefault = 350;
    [SerializeField] private float countMoneyDefault = 1000;
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

    public void UpdateInformationPlayer(string namePlayerGet, int dayPlayerGet)
    {
        this.namePlayer = namePlayerGet;
        this.day = dayPlayerGet;
        UIManager.instance.uiInformation.Init(namePlayer, day);
        //Todo update 
    }

    public void UpdateResourcePlayer(AssetData assetData)
    {
        this.assetData = assetData;
        InitResourcePlayer(assetData);
        UIManager.instance.UpdateUIViewItems(assetData);
    }

    public void UpdateBuildPlayer(BuildData buildData)
    {
        UIManager.instance.uiViewListGround.UpdateListView(buildData);
    }
    void InitResourcePlayer(AssetData assetData)
    {
        // Khởi tạo tài nguyên ban đầu cho người chơi
        initialResources = new Dictionary<ItemType, int>
        {
            { ItemType.Food, assetData.GetAssetCountByType(TypeObject.FOOD) },
            { ItemType.Iron, assetData.GetAssetCountByType(TypeObject.IRON) },
            { ItemType.Gold, assetData.GetAssetCountByType(TypeObject.GOLD) },
            { ItemType.Melee, assetData.GetAssetCountByType(TypeObject.MELEE) },
            { ItemType.Arrow, assetData.GetAssetCountByType(TypeObject.ARROW) },
            { ItemType.Cavalry, assetData.GetAssetCountByType(TypeObject.CAVALRY) },
            { ItemType.Citizen, assetData.GetAssetCountByType(TypeObject.CITIZEN) },

        };
        
        //assetPlayer = new AssetPlayer(assetData.countMoney, initialResources);
    }
    public void UpDayPlayer()
    {
        day += 1;
        UIManager.instance.uiInformation.UpdateDayPlayer(day);
        //UIManager.instance.UpdateUIViewItems();
        SoundManager.instance.PlayDaySound();
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
    
    public float GetResourceAmount(ItemType itemType)
    {
        return assetPlayer.GetResourceAmount(itemType);
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
    private float money;
    private Dictionary<ItemType, int> resources; // Lưu trữ tài nguyên
    public AssetPlayer(int money, Dictionary<ItemType, int> initialResources)
    {
        this.money = money;
        this.resources = new Dictionary<ItemType, int>(initialResources);
    }

    public float Money => money;

    public void AddMoney(float amount)
    {
        //UIManager.instance.UpdateUIViewItems();
        money += amount;
    }

    public bool AddItem(ItemType itemType, float price, float amount)
    {
        if (price < 0 || amount <= 0 || money < price * amount) return false;

        // Trừ tiền và thêm tài nguyên
        money -= price * amount;
        if (resources.ContainsKey(itemType))
        {
            //resources[itemType] += amount;
        }
        else
        {
            //resources[itemType] = amount; // Nếu tài nguyên chưa tồn tại
        }
        //UIManager.instance.UpdateUIViewItems();
        return true;
    }

    public bool RemoveItem(ItemType itemType, float price, float amount)
    {
        if (price < 0 || amount <= 0) return false;

        // Kiểm tra tồn tại tài nguyên và đủ số lượng để bán
        if (resources.ContainsKey(itemType) && resources[itemType] >= amount)
        {
            //resources[itemType] -= amount;
            money += price * amount;
            //UIManager.instance.UpdateUIViewItems();
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

