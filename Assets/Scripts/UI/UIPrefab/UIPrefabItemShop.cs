using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPrefabItemShop : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI nameObj;
    public TextMeshProUGUI statusObj;
    public TextMeshProUGUI countObj;
    public TextMeshProUGUI priceObj;
    public Button handleObj;

    public void InitItemShop(TypeItemTrade typeTrade, Sprite imgItem, string nameItem, float countItem, float priceItem, UnityAction callback)
    {
        CleanItemShop();
        if (typeTrade == TypeItemTrade.Buy)
        {
            img.sprite = imgItem;
            nameObj.text = nameItem;
            
            if (countItem > 0) statusObj.text = "Còn hàng";
            else statusObj.text = "Hết hàng";
            
            countObj.text = "Số lượng: " + countItem.ToString();
            priceObj.text = "Giá mua: " + priceItem.ToString();
            handleObj.onClick.AddListener(callback);
            handleObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Mua 1";
        }
    }

    void CleanItemShop()
    {
        img.sprite = null;
        nameObj.text = "";
        statusObj.text = "";
        countObj.text = "";
        priceObj.text = "";
        handleObj.onClick.RemoveAllListeners();
    }
}
