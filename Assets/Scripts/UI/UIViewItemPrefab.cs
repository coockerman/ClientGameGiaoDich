using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIViewItemPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TypeObj typeObj;
    public ItemType itemType;
    public SolierType solierType;
    public string nameVP = "Tiền";
    public Image img;
    public TextMeshProUGUI countText;
    
    public void Init(ItemType itemType, string nameVP, Sprite sprite, int count)
    {
        this.itemType = itemType;
        this.nameVP = nameVP;
        img.sprite = sprite;
        countText.text = count.ToString();
    }
    
    public void Init(TypeObj typeObj, ItemType itemType, string nameVP, Sprite sprite, int count)
    {
        this.typeObj = typeObj;
        this.itemType = itemType;
        this.nameVP = nameVP;
        img.sprite = sprite;
        countText.text = count.ToString();
    }
    public void Init(TypeObj typeObj, SolierType solierType, string nameVP, Sprite sprite, int count)
    {
        this.typeObj = typeObj;
        this.solierType = solierType;
        this.nameVP = nameVP;
        img.sprite = sprite;
        countText.text = count.ToString();
    }

    public void Init(int count)
    {
        countText.text = count.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (nameVP != "")
        {
            UIManager.instance.uiInfoVp.OnInfoVP(nameVP, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.uiInfoVp.OffInfoVP();
    }
}
