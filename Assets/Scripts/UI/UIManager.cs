using UnityEditor.PackageManager;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        public void BuyGold()
        {
                GameManager.instance.RequestBuy(true, ItemType.Gold, 20, 10);
        }
        public void SellGold()
        {
                GameManager.instance.RequestSell(true, ItemType.Gold, 20, 10);
        }
}