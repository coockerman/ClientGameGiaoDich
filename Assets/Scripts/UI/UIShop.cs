using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    public UIPrefabItemShop prefabItemShop;
    public Transform shopListBuy;
    public Transform shopListSell;
    public Button buyButtonListShop;
    public Button sellButtonListShop;
    private TypeItemTrade typeTrade = TypeItemTrade.Buy;
    private List<UIPrefabItemShop> itemShopListBuy = new List<UIPrefabItemShop>();
    private List<UIPrefabItemShop> itemShopListSell = new List<UIPrefabItemShop>();
    private UpdateStoreData dataStore;
    private List<ScriptableObj> listImgItem = new List<ScriptableObj>();

    private void Start()
    {
        buyButtonListShop.onClick.AddListener(OnShopListBuy);
        sellButtonListShop.onClick.AddListener(OnShopListSell);
    }

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

    public void OnShopListBuy()
    {
        shopListBuy.gameObject.SetActive(true);
        shopListSell.gameObject.SetActive(false);
    }

    public void OnShopListSell()
    {
        shopListBuy.gameObject.SetActive(false);
        shopListSell.gameObject.SetActive(true);
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
            if (index < itemShopListBuy.Count) // Kiểm tra index hợp lệ
            {
                UIPrefabItemShop itemShop = itemShopListBuy[index];
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
            if (index < itemShopListSell.Count) // Kiểm tra index hợp lệ
            {
                UIPrefabItemShop itemShop = itemShopListSell[index];
                ScriptableObj objFind = FindScriptableObject(type);

                if (objFind != null) // Chỉ cập nhật nếu tìm thấy ScriptableObj
                {
                    float priceSell = 0;

                    // Lấy dữ liệu từ dataStore tương ứng
                    switch (type)
                    {
                        case ItemType.Gold:
                            priceSell = dataStore.itemGold.priceSell;
                            break;

                        case ItemType.Iron:
                            priceSell = dataStore.itemIron.priceSell;
                            break;

                        case ItemType.Food:
                            priceSell = dataStore.itemFood.priceSell;
                            break;
                    }

                    // Cập nhật thông tin UI
                    itemShop.InitItemShop(
                        TypeItemTrade.Sell,
                        objFind.sprite,
                        objFind.nameObj,
                        type,
                        priceSell,
                        () => SellOneItem(type, priceSell)
                    );
                }
            }
        }
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
                    CreateBuyItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemGold.priceBuy, dataStore.itemGold.countInStock, ItemType.Gold);
                }
                else if (itemImg.itemType == ItemType.Iron)
                {
                    CreateBuyItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemIron.priceBuy, dataStore.itemIron.countInStock, ItemType.Iron);
                }
                else if (itemImg.itemType == ItemType.Food)
                {
                    CreateBuyItemShopUI(TypeItemTrade.Buy, itemImg, dataStore.itemFood.priceBuy, dataStore.itemFood.countInStock, ItemType.Food);
                }
            }
        }
    }

    void CreateBuyItemShopUI(TypeItemTrade tradeType, ScriptableObj itemImg, float price, int countInStock, ItemType itemType)
    {
        UIPrefabItemShop itemShop = Instantiate(prefabItemShop, shopListBuy.transform);
        itemShop.InitItemShop(
            tradeType, 
            itemImg.sprite, 
            itemImg.nameObj, 
            countInStock, 
            price, 
            () => BuyOneItem(itemType, price)
        );
        itemShopListBuy.Add(itemShop);
    }

    void InitDataSell()
    {
        foreach (ScriptableObj itemImg in listImgItem)
        {
            if (itemImg.typeObj == TypeObj.Item)
            {
                // Tìm ScriptableObj tương ứng để khởi tạo
                if (itemImg.itemType == ItemType.Gold)
                {
                    CreateSellItemShopUI(TypeItemTrade.Sell, itemImg, dataStore.itemGold.priceSell, ItemType.Gold);
                }
                else if (itemImg.itemType == ItemType.Iron)
                {
                    CreateSellItemShopUI(TypeItemTrade.Sell, itemImg, dataStore.itemIron.priceSell, ItemType.Iron);
                }
                else if (itemImg.itemType == ItemType.Food)
                {
                    CreateSellItemShopUI(TypeItemTrade.Sell, itemImg, dataStore.itemFood.priceSell, ItemType.Food);
                }
            }
        }
    }

    void CreateSellItemShopUI(TypeItemTrade tradeType, ScriptableObj itemImg, float priceSell, ItemType itemType)
    {
        UIPrefabItemShop itemShop = Instantiate(prefabItemShop, shopListSell.transform);
        itemShop.InitItemShop(
            tradeType, 
            itemImg.sprite, 
            itemImg.nameObj, 
            itemType, 
            priceSell, 
            () => SellOneItem(itemType, priceSell)
        );
        itemShopListSell.Add(itemShop);
    }
    
    void BuyOneItem(ItemType itemType, float price)
    {
        GameManager.instance.RequestBuy(true, itemType, (int) price, 1);
    }

    void SellOneItem(ItemType itemType, float price)
    {
        GameManager.instance.RequestSell(true, itemType, (int) price, 1);
    }

    public void OnShop()
    {
        CanvasGroup canvasGroup = GetOrAddCanvasGroup();
        gameObject.SetActive(true); // Bật đối tượng

        // Reset trạng thái alpha và scale trước khi mở
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.zero;

        // Tạo hiệu ứng làm mờ dần và phóng to
        canvasGroup.DOFade(1, 0.5f); // Làm mờ dần trong 0.5 giây
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // Phóng to với easing mượt
    }

    public void ExitShop()
    {
        CanvasGroup canvasGroup = GetOrAddCanvasGroup();

        // Tạo hiệu ứng làm mờ dần và thu nhỏ
        canvasGroup.DOFade(0, 0.5f); // Làm mờ dần trong 0.5 giây
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack) // Thu nhỏ mượt
            .OnComplete(() => gameObject.SetActive(false)); // Tắt đối tượng sau khi hiệu ứng kết thúc
    }

    private CanvasGroup GetOrAddCanvasGroup()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        return canvasGroup;
    }
}
