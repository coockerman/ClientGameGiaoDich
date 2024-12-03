using UnityEditor.PackageManager;
using UnityEngine;

public class UIManager : MonoBehaviour
{
        public void BuyGold()
        {
                AbstractData newBuy = new AbstractData(true, ItemType.Gold, 20, 100);
                RequestPacket newRequest = new RequestPacket(PacketType.Buy, newBuy);
                ClientManager.Instance.HandelDataAndSend(newRequest);
        }
}