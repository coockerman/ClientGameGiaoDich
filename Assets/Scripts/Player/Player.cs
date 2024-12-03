using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    AssetPlayer assetPlayer;

    private void Awake()
    {
        instance = this;
        assetPlayer = new AssetPlayer(100000, 50, 100, 200);
    }

        public bool CheckAddAsset(ItemType itemType, float price, float amount)
        {
            Debug.Log(assetPlayer.Money + "/" + assetPlayer.Gold + "/" + assetPlayer.Iron + "/" + assetPlayer.Food);
            return assetPlayer.AddItem(itemType, price, amount);
        }

        public bool CheckRemoveAsset(ItemType itemType, float price, float amount)
        {
            return assetPlayer.SellItem(itemType, price, amount);
        }
}

public class AssetPlayer
{
    private float money;
    private float gold;
    private float iron;
    private float food;
    
    public AssetPlayer(float money, float gold, float iron, float food)
    {
        this.money = money;
        this.gold = gold;
        this.iron = iron;
        this.food = food;
    }

    public float Money { get => money; }
    public float Gold { get => gold; }
    public float Iron { get => iron; }
    public float Food { get => food; }

    public bool AddItem(ItemType itemType, float price, float amount)
    {
        if (money < price * amount) return false;
        RemoveMoney(price * amount);
        
        if (itemType == ItemType.Gold)
            AddGold(amount);
        else if(itemType == ItemType.Iron)
            AddIron(amount);
        else if(itemType == ItemType.Food)
            AddFood(amount);
        
        return true;
    }

    public bool SellItem(ItemType itemType, float price, float amount)
    {
        if (itemType == ItemType.Gold)
            return RemoveGold(amount);
        else if (itemType == ItemType.Iron)
            return RemoveIron(amount);
        else if (itemType == ItemType.Food)
            return RemoveFood(amount);
        
        AddMoney(price * amount);
        return true;
    }
    
    public void AddMoney(float amount)
    {
        money += amount;
    }

    public bool RemoveMoney(float amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        return false;
    }

    public void AddGold(float amount)
    {
        gold += amount;
    }

    public bool RemoveGold(float amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    public void AddIron(float amount)
    {
        iron += amount;
    }

    public bool RemoveIron(float amount)
    {
        if (iron >= amount)
        {
            iron -= amount;
            return true;
        }
        return false;
    }

    public void AddFood(float amount)
    {
        food += amount;
    }

    public bool RemoveFood(float amount)
    {
        if (food >= amount)
        {
            food -= amount;
            return true;
        }
        return false;
    }
}
