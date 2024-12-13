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

        public void HandleGetNamePlayer(string namePlayer)
        {
                if (namePlayer != "")
                {
                        Player.instance.SetupNamePlayer(namePlayer);
                }
                else
                {
                        Debug.Log("Can get name player");
                }
        }

        public void HandelMessagePlayer(string namePlayer, string message)
        {
                if (namePlayer != Player.instance.NamePlayer)
                {
                        UIChat.instance.CheckAddMessageOpponent(namePlayer, message);
                }
        }
        public void RequestBuy(bool status, ItemType itemType, int price, int count)
        {
                AbstractData newBuy = new AbstractData(status, itemType, price, count);
                RequestPacket newRequest = new RequestPacket(PacketType.Buy, newBuy);
                ClientManager.Instance.HandelDataAndSend(newRequest, true);
        }
        public void RequestSell(bool status, ItemType itemType, int price, int count)
        {
                AbstractData newSell = new AbstractData(status, itemType, price, count);
                RequestPacket newRequest = new RequestPacket(PacketType.Sell, newSell);
                ClientManager.Instance.HandelDataAndSend(newRequest, true);
        }

        public void RequestUpdateStore()
        {
                RequestPacket request = new RequestPacket(PacketType.UpdateStore);
                ClientManager.Instance.HandelDataAndSend(request, false);
        }

        public void RequestMessage(string namePlayer, string message)
        {
                RequestPacket request = new RequestPacket(PacketType.MessagePlayer, namePlayer, message);
                ClientManager.Instance.HandelDataAndSend(request, false);
        }
        public void RequestBrankup()
        {
                RequestPacket request = new RequestPacket(PacketType.Bankrupt);
                ClientManager.Instance.HandelDataAndSend(request, false);
        }

        public void RequestDayPlay()
        {
                RequestPacket request = new RequestPacket(PacketType.DayPlay);
                ClientManager.Instance.HandelDataAndSend(request, false);
        }

        public void RequestAttackPlayer()
        {
                RequestPacket request = new RequestPacket(PacketType.AttackPlayer);
                ClientManager.Instance.HandelDataAndSend(request, false);
        }
        

        
}