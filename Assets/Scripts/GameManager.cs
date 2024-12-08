using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        private void Awake()
        {
                instance = this;
        }

        public void HandleBuy(AbstractData data)
        {
                if (data.isStatus == true)
                {
                        Player.instance.CheckAddAsset(data.itemType, data.price, data.count);
                }
                else
                {
                        Debug.Log("Hết đồ");
                }
        }
        public void HandleSell(AbstractData data)
        {
                if (data.isStatus == true)
                {
                        Player.instance.CheckRemoveAsset(data.itemType, data.price, data.count);
                }
        }

        public void HandleUpdateStore(UpdateStoreData data)
        {
                if (data != null)
                {
                        Debug.Log(data);
                }
        }
        public void RequestBuy(bool status, ItemType itemType, int price, int count)
        {
                AbstractData newBuy = new AbstractData(status, itemType, price, count);
                RequestPacket newRequest = new RequestPacket(PacketType.Buy, newBuy);
                ClientManager.Instance.HandelDataAndSend(newRequest);
        }
        public void RequestSell(bool status, ItemType itemType, int price, int count)
        {
                AbstractData newSell = new AbstractData(status, itemType, price, count);
                RequestPacket newRequest = new RequestPacket(PacketType.Sell, newSell);
                ClientManager.Instance.HandelDataAndSend(newRequest);
        }
        public void RequestBrankup()
        {
                
        }
        public void HandleResultAttack()
        {
                
        }
        public void HandleGetPlayerCanAttack()
        {
                
        }

        
}