using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPrefabItemShop : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI nameObj;
    public TextMeshProUGUI statusObj;
    public TextMeshProUGUI countObj;
    public TextMeshProUGUI priceObj;
    public Button handleOneObj;
    public Button handleTenObj;
    public void InitItemShop(TypeItemTrade typeTrade, Sprite imgItem, string nameItem, float countItem, float priceItem, UnityAction callOne, UnityAction callTen)
    {
        CleanItemShop();
        if (typeTrade == TypeItemTrade.Buy)
        {
            img.sprite = imgItem;
            nameObj.text = nameItem;

            if (countItem > 0)
            {
                statusObj.text = "Còn hàng";
                if (Player.instance.GetMoneyAmount() >= 1 * priceItem)
                {
                    handleOneObj.onClick.AddListener(callOne);
                }
            }
            else
            {
                statusObj.text = "Hết hàng";
            }
            if (Player.instance.GetMoneyAmount() >= 10 * priceItem && countItem >=10)
            {
                handleTenObj.onClick.AddListener(callTen);
            }
            
            countObj.text = "Số lượng: " + countItem.ToString();
            priceObj.text = "Giá mua: " + priceItem.ToString();
            
            handleOneObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mua x1";
            handleTenObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mua x10";
        }
    }
    public void InitItemShop(TypeItemTrade typeTrade, Sprite imgItem, string nameItem, ItemType itemType, float priceSell, UnityAction callOne, UnityAction callTen)
    {
        CleanItemShop();
        if (typeTrade == TypeItemTrade.Sell)
        {
            img.sprite = imgItem;
            nameObj.text = nameItem;

            if (Player.instance.GetResourceAmount(itemType) > 0)
            {
                statusObj.text = "Còn hàng";
                handleOneObj.onClick.AddListener(callOne);
            }
            else
            {
                statusObj.text = "Hết hàng";
            }

            if (Player.instance.GetResourceAmount(itemType) >= 10)
            {
                handleTenObj.onClick.AddListener(callTen);
            }
            
            countObj.text = "Bạn có: " + Player.instance.GetResourceAmount(itemType).ToString();
            priceObj.text = "Giá bán: " + priceSell.ToString();
            
            handleOneObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bán x1";
            handleTenObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bán x10";
        }
    }
    

    void CleanItemShop()
    {
        img.sprite = null;
        nameObj.text = "";
        statusObj.text = "";
        countObj.text = "";
        priceObj.text = "";
        handleOneObj.onClick.RemoveAllListeners();
        handleTenObj.onClick.RemoveAllListeners();
    }
}
