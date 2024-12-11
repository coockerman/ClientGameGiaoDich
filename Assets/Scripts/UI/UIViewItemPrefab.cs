using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIViewItemPrefab : MonoBehaviour
{
    public TypeObj typeObj;
    public ItemType itemType;
    public SolierType solierType;
    public Image img;
    public TextMeshProUGUI countText;
    
    public void Init(TypeObj typeObj, ItemType itemType,Sprite sprite, int count)
    {
        this.typeObj = typeObj;
        this.itemType = itemType;
        img.sprite = sprite;
        countText.text = count.ToString();
    }
    public void Init(TypeObj typeObj, SolierType solierType,Sprite sprite, int count)
    {
        this.typeObj = typeObj;
        this.solierType = solierType;
        img.sprite = sprite;
        countText.text = count.ToString();
    }

    public void Init(int count)
    {
        countText.text = count.ToString();
    }
}
