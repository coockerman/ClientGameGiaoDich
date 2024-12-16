using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    public UIPrefabItemShop prefabItemShop;
    public Transform shopList;
    
    private TypeItemTrade typeTrade = TypeItemTrade.Buy;
    private List<UIPrefabItemShop> itemShopList = new List<UIPrefabItemShop>();
    private UpdateStoreData dataStore;
    private List<ScriptableObj> listImgItem = new List<ScriptableObj>();
    

    public void UpdateStoreData(UpdateStoreData data, List<ScriptableObj> listImgItem)
    {
        this.listImgItem = listImgItem;
        dataStore = data;
        InitUIData();
    }
    
    public void UpdateStoreData(UpdateStoreData data)
    {
        dataStore = data;
        UpdateUIData();
    }
    
    void InitUIData()
    {
        InitDataBuy();
        InitDataSell();
    }

    void UpdateUIData()
    {
        UpdateDataBuy();
        UpdateDataSell();
    }

    void UpdateDataBuy()
    {
        // Tạo danh sách các cặp ItemType và vị trí tương ứng trong itemShopList
        var itemTypes = new (ItemType type, int index)[]
        {
            (ItemType.Gold, 0),
            (ItemType.Iron, 1),
            (ItemType.Food, 2)
        };

        // Lặp qua từng loại ItemType để cập nhật dữ liệu
        foreach (var (type, index) in itemTypes)
        {
            if (index < itemShopList.Count) // Kiểm tra index hợp lệ
            {
                UIPrefabItemShop itemShop = itemShopList[index];
                ScriptableObj objFind = FindScriptableObject(type);

                if (objFind != null) // Chỉ cập nhật nếu tìm thấy ScriptableObj
                {
                    int countInStock = 0;
                    float priceBuy = 0;

                    // Lấy dữ liệu từ dataStore tương ứng
                    switch (type)
                    {
                        case ItemType.Gold:
                            countInStock = dataStore.itemGold.countInStock;
                            priceBuy = dataStore.itemGold.priceBuy;
                            break;

                        case ItemType.Iron:
                            countInStock = dataStore.itemIron.countInStock;
                            priceBuy = dataStore.itemIron.priceBuy;
                            break;

                        case ItemType.Food:
                            countInStock = dataStore.itemFood.countInStock;
                            priceBuy = dataStore.itemFood.priceBuy;
                            break;
                    }

                    // Cập nhật thông tin UI
                    itemShop.InitItemShop(
                        TypeItemTrade.Buy,
                        objFind.sprite,
                        objFind.nameObj,
                        countInStock,
                        priceBuy,
                        () => BuyOneItem(type, priceBuy)
                    );
                }
            }
        }
    }

    void UpdateDataSell()
    {
        
    }
    ScriptableObj FindScriptableObject(ItemType itemType)
    {
        foreach (ScriptableObj obj in listImgItem)
        {
            if (obj.typeObj == TypeObj.Item)
            {
                if (obj.itemType == itemType)
                {
                    return obj;
                }
            }
        }
        return null; // Trả về null nếu không tìm thấy
    }

    void InitDataBuy()
    {
        foreach (ScriptableObj itemImg in listImgItem)
        {
            if (itemImg.typeObj == TypeObj.Item)
            {
                // Tìm ScriptableObj tương ứng để khởi tạo
                if (itemImg.itemType == ItemType.Gold)
                {
                    CreateItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemGold.priceBuy, dataStore.itemGold.countInStock, ItemType.Gold);
                }
                else if (itemImg.itemType == ItemType.Iron)
                {
                    CreateItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemIron.priceBuy, dataStore.itemIron.countInStock, ItemType.Iron);
                }
                else if (itemImg.itemType == ItemType.Food)
                {
                    CreateItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemFood.priceBuy, dataStore.itemFood.countInStock, ItemType.Food);
                }
            }
        }
    }

    void CreateItemShopUI(TypeItemTrade tradeType, ScriptableObj itemImg, float price, int countInStock, ItemType itemType)
    {
        UIPrefabItemShop itemShop = Instantiate(prefabItemShop, shopList.transform);
        itemShop.InitItemShop(
            tradeType, 
            itemImg.sprite, 
            itemImg.nameObj, 
            countInStock, 
            price, 
            () => BuyOneItem(itemType, price)
        );
        itemShopList.Add(itemShop);
    }

    void InitDataSell()
    {
        
    }

    void BuyOneItem(ItemType itemType, float price)
    {
        GameManager.instance.RequestBuy(true, itemType, (int) price, 1);
    }

    public void ExitShop()
    {
        gameObject.SetActive(false);
    }
}
