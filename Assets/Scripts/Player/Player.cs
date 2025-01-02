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
    private int money = 0;
    public string Username => username;
    public string NamePlayer => namePlayer;

    public string UsernameWaitRegister { get => usernameWaitRegister; set => usernameWaitRegister = value; }
    public string NamePlayerWaitRegister { get => namePlayerWaitRegister; set => namePlayerWaitRegister = value; }
    
    public int Day => day;
    public Dictionary<ItemType, int> initialResources = new Dictionary<ItemType, int>();
    
    
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

    public void LogoutPlayer()
    {
        username = "";
        usernameWaitRegister = "";
        namePlayer = "";
        namePlayerWaitRegister = "";
        day = 0;
        assetData = null;
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
        this.money = assetData.countMoney;
        InitResourcePlayer(assetData);
        UIManager.instance.UpdateUIViewItems(assetData);
    }

    public void UpdateBuildPlayer(BuildData buildData)
    {
        UIManager.instance.uiViewListGround.UpdateListView(buildData);
    }
    void InitResourcePlayer(AssetData assetData)
    {
        // Cập nhật tài nguyên cho người chơi
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
    }
    
    public int GetResourceAmount(ItemType itemType)
    {
        return initialResources[itemType];
    }
    
    public float GetMoneyAmount()
    {
        return money;
    }
}

