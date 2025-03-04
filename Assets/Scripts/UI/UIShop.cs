using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
    public TextMeshProUGUI nameShop;
    private TypeItemTrade typeTrade = TypeItemTrade.Buy;
    private List<UIPrefabItemShop> itemShopListBuy = new List<UIPrefabItemShop>();
    private List<UIPrefabItemShop> itemShopListSell = new List<UIPrefabItemShop>();
    //private UpdateStoreData dataStore;
    private List<ScriptableObj> listImgItem = new List<ScriptableObj>();

    private void Start()
    {
        buyButtonListShop.onClick.AddListener(OnShopListBuy);
        sellButtonListShop.onClick.AddListener(OnShopListSell);
    }

    public void UpdateStoreData(UpdateStoreData data, List<ScriptableObj> listImgItem)
    {
        this.listImgItem = listImgItem;
        //dataStore = data;
        InitUIData(data);
    }
    
    public void UpdateStoreData(UpdateStoreData data)
    {
        UpdateUIData(data);
    }

    void OnShopListBuy()
    {
        nameShop.text = "Cửa hàng mua vật phẩm";
        shopListBuy.gameObject.SetActive(true);
        shopListSell.gameObject.SetActive(false);
    }

    void OnShopListSell()
    {
        nameShop.text = "Bán vật phẩm";
        shopListBuy.gameObject.SetActive(false);
        shopListSell.gameObject.SetActive(true);
    }
    void InitUIData(UpdateStoreData data)
    {
        InitDataBuy(data);
        InitDataSell(data);
    }

    void UpdateUIData(UpdateStoreData dataStore)
    {
        UpdateDataBuy(dataStore);
        UpdateDataSell(dataStore);
    }

    void UpdateDataBuy(UpdateStoreData dataStore)
    {
        // Tạo danh sách các cặp ItemType và vị trí tương ứng trong itemShopList
        var itemTypes = new (ItemType type, int index)[]
        {
            (ItemType.Food, 0),
            (ItemType.Iron, 1),
            (ItemType.Gold, 2)
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
                        () => BuyItem(type, 1),
                        () => BuyItem(type, 10)
                    );
                }
            }
        }
    }

    void UpdateDataSell(UpdateStoreData dataStore)
    {
        // Tạo danh sách các cặp ItemType và vị trí tương ứng trong itemShopList
        var itemTypes = new (ItemType type, int index)[]
        {
            (ItemType.Food, 0),
            (ItemType.Iron, 1),
            (ItemType.Gold, 2)
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
                        () => SellItem(type, 1),
                        () => SellItem(type, 10)
                    );
                }
            }
        }
    }
    ScriptableObj FindScriptableObject(ItemType itemType)
    {
        foreach (ScriptableObj obj in listImgItem)
        {
            if (obj.itemType == itemType)
            {
                return obj;
            }
        }
        return null; // Trả về null nếu không tìm thấy
    }

    void InitDataBuy(UpdateStoreData dataStore)
    {
        foreach (ScriptableObj itemImg in listImgItem)
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

    void CreateBuyItemShopUI(TypeItemTrade tradeType, ScriptableObj itemImg, float price, int countInStock, ItemType itemType)
    {
        UIPrefabItemShop itemShop = Instantiate(prefabItemShop, shopListBuy.transform);
        itemShop.InitItemShop(
            tradeType, 
            itemImg.sprite, 
            itemImg.nameObj, 
            countInStock, 
            price, 
            () => BuyItem(itemType,  1),
            () => BuyItem(itemType, 10)
        );
        itemShopListBuy.Add(itemShop);
    }

    void InitDataSell(UpdateStoreData dataStore)
    {
        foreach (ScriptableObj itemImg in listImgItem)
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

    void CreateSellItemShopUI(TypeItemTrade tradeType, ScriptableObj itemImg, float priceSell, ItemType itemType)
    {
        UIPrefabItemShop itemShop = Instantiate(prefabItemShop, shopListSell.transform);
        itemShop.InitItemShop(
            tradeType, 
            itemImg.sprite, 
            itemImg.nameObj, 
            itemType, 
            priceSell, 
            () => SellItem(itemType, 1),
            () => SellItem(itemType, 10)
        );
        itemShopListSell.Add(itemShop);
    }
    
    void BuyItem(ItemType itemType, float count)
    {
        SoundManager.instance.PlaySoundBuy();
        GameManager.instance.RequestBuy(itemType, (int)count);
    }

    
    void SellItem(ItemType itemType, float count)
    {
        SoundManager.instance.PlaySoundSell();
        GameManager.instance.RequestSell(itemType, (int)count);
    }
    
    public void OnShop()
    {
        CanvasGroup canvasGroup = GetOrAddCanvasGroup();
        gameObject.SetActive(true); // Bật đối tượng
        OnShopListBuy();
        
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
