using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Hàng đợi các hành động cần xử lý trên main thread
    private readonly Queue<Action> mainThreadActions = new Queue<Action>();

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        UIManager.instance.uiConnection.OnConnectUI();
    }

    private void Update()
    {
        // Xử lý tất cả các hành động trong hàng đợi
        while (mainThreadActions.Count > 0)
        {
            mainThreadActions.Dequeue()?.Invoke();
        }
    }

    // Hàm tiện ích để thêm hành động vào hàng đợi
    public void RunOnMainThread(Action action)
    {
        Debug.Log("RunOnMainThread");
        lock (mainThreadActions)
        {
            mainThreadActions.Enqueue(action);
        }
    }

    public void HandelConnection(string urlConnect)
    {
        Player.instance.InitResourcePlayer();
        UIManager.instance.FinishConnectionUI(urlConnect);
    }
    // Các hàm Handle...
    public void HandleBuy(AbstractData data)
    {
        RunOnMainThread(() =>
        {
            if (data.isStatus)
            {
                Player.instance.CheckAddAsset(data.itemType, data.price, data.count);
            }
            else
            {
                Debug.Log("Hết đồ");
            }
        });
    }

    public void HandleSell(AbstractData data)
    {
        RunOnMainThread(() =>
        {
            if (data.isStatus)
            {
                Player.instance.CheckRemoveAsset(data.itemType, data.price, data.count);
            }
        });
    }

    public void HandleUpdateStore(UpdateStoreData data)
    {
        RunOnMainThread(() =>
        {
            if (data != null)
            {
                UIManager.instance.UpdateStoreData(data);
            }
        });
    }

    public void HandleGetNamePlayer(string namePlayer)
    {
        RunOnMainThread(() =>
        {
            if (!string.IsNullOrEmpty(namePlayer))
            {
                Player.instance.SetupNamePlayer(namePlayer);
                
            }
            else
            {
                Debug.Log("Can't get name player");
            }
        });
    }

    public void HandleUpdateUIPlayerCanAttack(List<InfoPlayer> listInfo)
    {
        RunOnMainThread(() =>
        {
            if (listInfo != null)
            {
                UIManager.instance.uiPK.InitUI(listInfo);
            }
        });
    }
    public void HandleMessagePlayer(string namePlayer, string message)
    {
        RunOnMainThread(() =>
        {
            if (namePlayer != Player.instance.NamePlayer)
            {
                UIManager.instance.uiChat.CheckAddMessageOpponent(namePlayer, message);
            }
        });
    }

    public void HandleRegisterPlayer(string namePlayer, bool isRegister)
    {
        RunOnMainThread(() =>
        {
            if (isRegister)
            {
                Player.instance.SetupNamePlayer(namePlayer);
                UIManager.instance.HandleUpdateUIRegisterFinish(namePlayer);
            }
            else
            {
                UIManager.instance.uiRegisterName.SetTextDialogRegister("Tên người chơi đã tồn tại", Color.red);
            }
        });
    }

    public void HandleFailConnection()
    {
        //Load lại game
        SceneManager.LoadScene(0);
    }

    public void HandleFailDeserializeJson()
    {
        
    }
    public void HandleFailGetMessage()
    {
        
    }
    // Các hàm Request gửi yêu cầu
    public void RequestBuy(bool status, ItemType itemType, int price, int count)
    {
        AbstractData newBuy = new AbstractData(status, itemType, count, price);
        RequestPacket newRequest = new RequestPacket(PacketType.Buy, newBuy);
        ClientManager.Instance.HandelDataAndSend(newRequest, true);
    }
    public void RequestSell(bool status, ItemType itemType, int price, int count)
    {
        AbstractData newSell = new AbstractData(status, itemType, count, price);
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

    public void RequestFindPlayerCanAttack()
    {
        RequestPacket request = new RequestPacket(PacketType.FindPlayerCanAttack);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestDayPlay(float dayPlay, SoldierData soldier)
    {
        RequestPacket request = new RequestPacket(PacketType.DayPlay, "" + dayPlay, soldier);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestAttackPlayer()
    {
        RequestPacket request = new RequestPacket(PacketType.AttackPlayer);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestRegisterPlayer(string namePlayer)
    {
        RequestPacket request = new RequestPacket(PacketType.RegisterPlayer, namePlayer);
        ClientManager.Instance.HandelDataAndSend(request, true);
    }
}
